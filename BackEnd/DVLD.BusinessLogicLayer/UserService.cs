using DVLD.BusinessLogicLayer.Enums;
using DVLD.BusinessLogicLayer.Enums.Person;
using DVLD.DataAccessLayer;
using DVLD.DataAccessLayer.DTOs.User;
using System.Security.Cryptography;
namespace DVLD.BusinessLogicLayer
{
    public class UserService
    {
        private readonly UserData _userData;
        private readonly PersonData _personData;
        public UserService(string connectionString)
        {
            _userData = new UserData(connectionString) ;
            _personData = new PersonData(connectionString) ;
        }
        public async Task<(LoginResponseDTO response,enUser userStatus)> Login(LoginRequestDTO Request) {
            GetUserDTO user = await _userData.GetByEmailAsync(Request.Email);
            if (user == null)
                return (null, enUser.NotFound);
            if (!user.IsActive)
                return (null, enUser.NotActive);

           string CurrentPassword = await _userData.GetCurrentPasswordAsync(user.UserID);
            if (CurrentPassword != Request.Password)
                return (null, enUser.InvalidEmailOrPassword);

            string NewToken = GenerateSecureToken();

            bool IsTokenUpdated= await _userData.UpdateTokenAsync(new UpdateTokenDTO() { UserID=user.UserID,Token=NewToken,ExpiryDate=DateTime.Now});

            if (IsTokenUpdated)
            {
                 LoginResponseDTO response = new LoginResponseDTO() { Token = NewToken ,TokenExpiredAt=DateTime.Now};

                return (response, enUser.success);
            }
            else
                return (null, enUser.Failed);
        }

        public async Task<(int NewUserID, enUser UserStatus)> AddNewAsync(AddNewUserDTO DTO)
        {
            bool HasData = await _personData.HasDataAsync(DTO.PersonID);
            if (HasData)
                return (-1, enUser.HasData);

            bool IsUserNameUsed = await _userData.IsUsernameUsed(DTO.UserName);
            if (IsUserNameUsed)
                return (-1, enUser.UsedBefore);

            int UserID = await _userData.AddNewAsync(DTO);

            if (UserID > 0)
                return (UserID, enUser.success);
            else
                return (-1, enUser.Failed);
        }

        public async Task<GetUserDTO> GetByEmailAsync(string Email)
        {
            GetUserDTO user = await _userData.GetByEmailAsync(Email);
            user.PersonInfo = await _personData.GetByIDAsync(user.PersonID);
            return user;
        }
        public async Task<GetUserDTO> GetByIDAsync(int UserID) {
           GetUserDTO user = await _userData.GetByIDAsync(UserID);
            user.PersonInfo = await _personData.GetByIDAsync(user.PersonID);
            return user;
        }
        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO Request) { return new LoginResponseDTO(); }
        public async Task<enUser> ChangePasswordAsync(ChangePasswordDTO DTO) { 
            GetUserDTO user = await _userData.GetByIDAsync(DTO.UserID);

            if (user == null)
                return enUser.NotFound;
            string CurrentPassword =await _userData.GetCurrentPasswordAsync(DTO.UserID);
            if (CurrentPassword != DTO.NewPassword)
                return enUser.OldPasswordIncorrect;

            bool IsPasswordChanged = await _userData.ChangePasswordAsync(DTO);
            if (IsPasswordChanged)
                return enUser.success;
            else 
                return enUser.Failed;

        }
        public async Task<enUser> ChangeActivation(ChangeUserActivationDTO DTO) {
            GetUserDTO user = await _userData.GetByIDAsync(DTO.UserID);

            if (user == null)
                return enUser.NotFound;
           bool IsUpdated=await _userData.UpdateActivation(DTO);

            if (IsUpdated)
                return enUser.success;
            else
                return enUser.Failed;
        }
        public async Task<enUser> DeleteAsync(int UserID) {
            GetUserDTO user =await _userData.GetByIDAsync(UserID);
            if (user == null)
                return enUser.NotFound;
            bool IsDeleted = await _userData.DeleteAsync(user.UserID);

            if (IsDeleted)
                return enUser.success;
            else
                return enUser.Failed;
        }
        public async Task<enUser> UpdateActivation(ChangeUserActivationDTO DTO) {
            bool IsUpdated= await _userData.UpdateActivation(DTO);

            if (IsUpdated)
                return enUser.success;
            else
                return enUser.Failed;
        }
        public async Task<enPersonStatus> GetRoleByTokenAsync(string token)
        {
            var role = await _userData.GetRoleByTokenAsync(token);

            var status = role == "Admin"
                     ? enPersonStatus.Authorized
                     : enPersonStatus.Unauthorized;

            return status;
        }
        public async Task<(string Role, enGetRoleByEmailStatus Status)> GetRoleByEmailAsync(string Email)
        {
            if (string.IsNullOrEmpty(Email))
                return (string.Empty, enGetRoleByEmailStatus.InvalidEmail);

            string Role = await _userData.GetRoleByEmailAsync(Email);

            if (string.IsNullOrEmpty(Role))
                return (string.Empty, enGetRoleByEmailStatus.Failed);

            return (Role, enGetRoleByEmailStatus.Success);
        }

        private static string GenerateSecureToken()
        {
            byte[] randomBytes = new byte[50];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(randomBytes);
            }
            return Convert.ToBase64String(randomBytes);
        }

    }
}
