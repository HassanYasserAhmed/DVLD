using DVLD.BusinessLogicLayer.Enums.Person;
using DVLD.BusinessLogicLayer.Enums;
using DVLD.DataAccessLayer;
using DVLD.DataAccessLayer.DTOs;
using DVLD.DataAccessLayer.DTOs.Person;

namespace DVLD.BusinessLogicLayer
{
    public class PersonService
    {
        private readonly PersonData _personData;
        private readonly FileService _fileService;
        public PersonService(string ConnectionString)
        {
            _personData = new PersonData(ConnectionString);
            _fileService = new FileService(ConnectionString);
        }
        public async Task<enValidationStatus> IsNationalNumberExists(string NationalNumber)
        {
            bool IsExists = await _personData.IsNationalNumberExistsAsync(NationalNumber);
            if (IsExists)
                return enValidationStatus.Exists;
            else
                return enValidationStatus.NotExists;
        }
        public async Task<enValidationStatus> IsEmailExistsAsync(string Email)
        {

            bool IsExists  = await _personData.IsEmailExistsAsync(Email.ToLower());
            if (IsExists)
                return enValidationStatus.Exists;
            else
                return enValidationStatus.NotExists;
        }  
        public async Task<enValidationStatus> IsPhoneExists(string Phone)
        {
            bool IsExists = await _personData.IsPhoneExistsAsync(Phone);
            if(IsExists)
                return enValidationStatus.Exists;

            return enValidationStatus.NotExists;
        }
        public async Task<(int NewPersonID, enAddPersonStatus Status)> AddAsync(AddNewPersonDTO dto)
        {
                if (!string.IsNullOrWhiteSpace(dto.Email) && await _personData.IsEmailExistsAsync(dto.Email))
                    return (-1, enAddPersonStatus.EmailAlreadyExists);

                if (!string.IsNullOrWhiteSpace(dto.Phone) && await _personData.IsPhoneExistsAsync(dto.Phone))
                    return (-1, enAddPersonStatus.PhoneAlreadyExists);

                if (!string.IsNullOrWhiteSpace(dto.NationalNumber) && await _personData.IsNationalNumberExistsAsync(dto.NationalNumber))
                    return (-1, enAddPersonStatus.NationalNumberAlreadyExists);

                if (GeneralMethods.CalculateAge(dto.DateOfBirth) < 18)
                    return (-1, enAddPersonStatus.AgeUnder18);

                int newPersonID = await _personData.AddAsync(dto);

                var status = newPersonID switch
                {
                    <= 0 => enAddPersonStatus.Failed,
                    _ => enAddPersonStatus.Success
                };

                return (newPersonID, status);
        }
        public async Task<(bool IsUpdated,enUpdatePersonStatus Status)> UpdateAsync(UpdatePersonDTO dto)
        {
                if (_personData.GetByIDAsync(dto.PersonID) == null)
                    return (false, enUpdatePersonStatus.UserNotFound);

                if (!string.IsNullOrWhiteSpace(dto.Email) && await _personData.IsEmailExistsAsync(dto.Email))
                    return (false, enUpdatePersonStatus.EmailAlreadyExists);

                if (!string.IsNullOrWhiteSpace(dto.Phone) && await _personData.IsPhoneExistsAsync(dto.Phone))
                    return (false, enUpdatePersonStatus.PhoneAlreadyExists);

                if (!string.IsNullOrWhiteSpace(dto.NationalNumber) && await _personData.IsNationalNumberExistsAsync(dto.NationalNumber))
                    return (false, enUpdatePersonStatus.NationalNumberAlreadyExists);

                if (GeneralMethods.CalculateAge(dto.DateOfBirth) < 18)
                    return (false, enUpdatePersonStatus.AgeUnder18);

                bool IsUpdated = await _personData.UpdateAsync(dto);

                var status = IsUpdated switch
                {
                     false => enUpdatePersonStatus.Failed,
                    _ => enUpdatePersonStatus.Success
                };

                return (IsUpdated, status);
        }
        public async Task<(bool IsDeleted, enDeletePersonStatus Status)> DeleteAsync(int personID)
        {
            var person = await _personData.GetByIDAsync(personID);

            if (person == null)
                return (false, enDeletePersonStatus.PersonNotFound);

            bool isDeleted = await _personData.DeleteAsync(personID);

            var status = isDeleted switch
            {
                true => enDeletePersonStatus.Success,
                false => enDeletePersonStatus.Failed
            };

            return (isDeleted, status);
        }
        public async Task<(bool IsAdded, enAddImageStatus status)> AddImageAsync(ProfileImageDTO DTO)
        {
           string NewImageName=await _fileService.UploadFileAsync(DTO.ImageBytes, DTO.ImageName);
            if (string.IsNullOrEmpty(NewImageName))
                return (false, enAddImageStatus.Failed);

             ChangeImageNameDTO ChangeImageDTO = new ChangeImageNameDTO()
                        { ImageName = NewImageName ,PersonID=DTO.PersonID};
            bool IsAdded =await _personData.ChangeImageNameAsync(ChangeImageDTO);

            if(!IsAdded)
                return (false, enAddImageStatus.Failed);

            return (true, enAddImageStatus.Success);
        }
        public async Task<(bool IsAdded, enUpdateImageStatus status)> UpdateImageAsync(ProfileImageDTO DTO)
        {
            GetPersonDTO person = await _personData.GetByIDAsync(DTO.PersonID);

            if(person == null)
                return (false, enUpdateImageStatus.PersonNotFound);

            if (string.IsNullOrEmpty(person.ImageName))
                return (false, enUpdateImageStatus.ImageNotFound);

            bool IsDeleted = _fileService.DeleteFile(person.ImageName);

            if (!IsDeleted)
                return (false, enUpdateImageStatus.FailedToDeleteOldImage);

            string NewImageName =await _fileService.UploadFileAsync(DTO.ImageBytes, DTO.ImageName);

            if (string.IsNullOrEmpty(NewImageName))
                return (false, enUpdateImageStatus.FailedToUploadNewImage);

            ChangeImageNameDTO changeDTO = new ChangeImageNameDTO()
                       { ImageName = NewImageName ,PersonID=person.PersonID};

            bool IsImageUpdated = await _personData.ChangeImageNameAsync(changeDTO);

            if (!IsImageUpdated)
                return (false, enUpdateImageStatus.Failed);

            return (true, enUpdateImageStatus.Success);
        }
        public async Task<(List<GetPersonDTO> People,enPersonStatus Status)> GetAllAsync()
        {
            List<GetPersonDTO> people = await _personData.GetAllAsync();

            if (people == null || people.Count == 0)
                return (new List<GetPersonDTO>(), enPersonStatus.NotFound);
           
            return (people,enPersonStatus.Found);
        }

        public async Task<(GetPersonDTO person, enGetPersonStatus Status)> GetByIDAsync(int PersonID)
        {
           GetPersonDTO person = await _personData.GetByIDAsync(PersonID);

            if (person == null)
                return (new GetPersonDTO(), enGetPersonStatus.Faild);

            return (person, enGetPersonStatus.Success);
        }
    }
}
