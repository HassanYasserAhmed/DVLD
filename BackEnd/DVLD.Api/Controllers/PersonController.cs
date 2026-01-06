using DVLD.Api.Requests;
using DVLD.BusinessLogicLayer;
using DVLD.BusinessLogicLayer.Enums.Person;
using DVLD.DataAccessLayer.DTOs.Person;
using DVLD.Api.Responses;
using Microsoft.AspNetCore.Mvc;
using DVLD.BusinessLogicLayer.Enums;

namespace DVLD.Api.Controllers
{
    [Route("api/People")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly PersonService _personService;
        private readonly UserService _userService;
        public PersonController(IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection");
            _personService = new PersonService(connectionString);
            _userService = new UserService(connectionString);
        }
        [HttpPost]
        public async Task<ActionResult> AddNewPersonAsync([FromBody] AddNewPersonRequest request)
        {
            try
            {
                var  roleStatus = await _userService.GetRoleByTokenAsync(request.Token);
                if (roleStatus == enPersonStatus.Unauthorized)
                    return Unauthorized(new Response { Message = "Unauthorized Access" });

                var (newPersonID, addPersonStatus) = await _personService.AddAsync(request.PersonData);

                var statusMessage = addPersonStatus switch
                {
                    enAddPersonStatus.Failed => "Failed To Add Person, Please Try Again",
                    enAddPersonStatus.EmailAlreadyExists => "Email Already Exists, Try another Email",
                    enAddPersonStatus.PhoneAlreadyExists => "Phone Already Exists, Try another Phone",
                    enAddPersonStatus.NationalNumberAlreadyExists => "National Number Already Exists, You Can't Use It Again",
                    enAddPersonStatus.AgeUnder18 => "The Age is Under 18, You Can't Add Person",
                    enAddPersonStatus.Success => null,
                    _ => "Something Went Wrong"
                };

                if (addPersonStatus == enAddPersonStatus.Success)
                    return Ok(new Response { Success = true, Message = "Person Added Successfully", Data = newPersonID });
                else
                    return BadRequest(new Response { Message = statusMessage });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { Message = ex.Message });
            }
        }
        [HttpPut]
        public async Task<ActionResult> UpdatePersonAsync([FromBody] UpdatePersonRequest request)
        {
            try
            {
                var  roleStatus = await _userService.GetRoleByTokenAsync(request.Token);
                if (roleStatus == enPersonStatus.Unauthorized)
                    return Unauthorized(new Response { Message = "Unauthorized Access" });

                var (IsUpdated, UpdatePersonStatus) = await _personService.UpdateAsync(request.PersonData);

                var statusMessage = UpdatePersonStatus switch
                {
                    enUpdatePersonStatus.Failed => "Failed To Update Person, Please Try Again",
                    enUpdatePersonStatus.EmailAlreadyExists => "Email Already Exists, Try another Email",
                    enUpdatePersonStatus.PhoneAlreadyExists => "Phone Already Exists, Try another Phone",
                    enUpdatePersonStatus.NationalNumberAlreadyExists => "National Number Already Exists, You Can't Use It Again",
                    enUpdatePersonStatus.AgeUnder18 => "The Age is Under 18, You Can't Add Person",
                    enUpdatePersonStatus.Success => null,
                    enUpdatePersonStatus.UserNotFound => $"User With ID {request.PersonData.PersonID} Not Found",
                    _ => "Something Went Wrong"
                };

                if (UpdatePersonStatus == enUpdatePersonStatus.Success)
                    return Ok(new Response { Success = true, Message = "Person Updated Successfully" });
                else
                    return BadRequest(new Response { Message = statusMessage });
            }
            catch (Exception ex)
            {
                return BadRequest(new Response { Message = ex.Message });
            }
        }
        [HttpDelete]
        public async Task<IActionResult> DeletePersonAsync([FromBody] DeletePersonRequest request)
        {
            if (request == null || string.IsNullOrWhiteSpace(request.Token))
                return BadRequest(new Response { Message = "Invalid request data" });

            var roleStatus = await _userService.GetRoleByTokenAsync(request.Token);

            if (roleStatus == enPersonStatus.Unauthorized)
                return Unauthorized(new Response { Message = "Unauthorized access" });

            var (isDeleted, deleteStatus) = await _personService.DeleteAsync(request.PersonID);

            return deleteStatus switch
            {
                enDeletePersonStatus.Success => Ok(new Response
                {
                    Success = true,
                    Message = "Person deleted successfully"
                }),

                enDeletePersonStatus.PersonNotFound => NotFound(new Response
                {
                    Message = "Person not found"
                }),

                _ => StatusCode(StatusCodes.Status500InternalServerError, new Response
                {
                    Message = "Failed to delete person"
                })
            };
        }
        [HttpGet]
        public async Task<IActionResult> GetPersonAsync([FromQuery] GetPersonRequest request)
        {
            var RoleStatus = await _userService.GetRoleByTokenAsync(request.Token);

            if (RoleStatus == enPersonStatus.Unauthorized)
                return BadRequest(new Response { Message = "Unauthorized Access" });
            (GetPersonDTO person,enGetPersonStatus personStatus) = await _personService.GetByIDAsync(request.PersonID);

            if (personStatus == enGetPersonStatus.Success)
                return Ok(new Response() { Success = true, Message = "Person Ge Successfully", Data = person });
            else
                return BadRequest(new Response() { Message = "Failed To Get Person" });
        }
        [HttpGet("GetAll")]
        public async Task<IActionResult> GetPeopleAsync([FromQuery] string Token)
        {
            enPersonStatus RoleStatus = await _userService.GetRoleByTokenAsync(Token);

            if (RoleStatus == enPersonStatus.Unauthorized)
                return Unauthorized(new Response { Message = "Unauthorized Access" });

            (List<GetPersonDTO> people, enPersonStatus personStatus) = await _personService.GetAllAsync();

            if (personStatus == enPersonStatus.Found)
                return Ok(new Response() { Success = true, Message = "People Gets Successfully", Data = people });
            else
                return NotFound(new Response() { Message = "People Not Not Found" });
        }
        [HttpPost("ProfileImage")]
        public async Task<IActionResult> AddProfileImageAsync([FromBody] AddImageRequest request)
        {
             enPersonStatus RoleStatus = await _userService.GetRoleByTokenAsync(request.Token);

            if (RoleStatus == enPersonStatus.Unauthorized)
                return BadRequest(new Response { Message = "Unauthorized Access" });

            (bool IsAdded, enAddImageStatus ImageStatus) = await _personService.AddImageAsync(request.DTO);
            if (ImageStatus == enAddImageStatus.Success)
                return Ok(new Response() { Success = true, Message = "Image Uploaded Successfully" });
            else
                return BadRequest(new Response() { Message = "Failed To UploadImage Image" });
        }
        [HttpPost("Validations/Email")]
        public async Task<ActionResult> IsEmailValidAsync([FromBody] string Email)
        {
            if(!GeneralMethods.IsValidEmail(Email))
                return BadRequest(new Response() { Message=$"{Email} is Not A Email Format"});
            enValidationStatus ValidationStatus =  await _personService.IsEmailExistsAsync(Email);
            if(ValidationStatus == enValidationStatus.NotExists)
                     return Ok(new Response() { Success = true, Message = "Email Is Valid" });

            return NotFound(new Response() { Message = "Invalid Email,Email Is Already Exist" });

        }
        [HttpPost("Validations/NationalNumber")]
        public async Task<ActionResult> IsNationalNumberValidASync([FromBody] string NationalNumber )
        {
            var ValidationStatus = await _personService.IsNationalNumberExists(NationalNumber);

            if (ValidationStatus == enValidationStatus.NotExists)
                return Ok(new Response() { Success = true, Message = "NationalNumber Is Valid" });

            return BadRequest(new Response() { Message = "Invalid NationalNumber "});
        }
        [HttpPost("Validations/Phone")]
        public async Task<ActionResult> IsPhoneValidAsync([FromBody] string Phone)
        {
            var ValidationStatus = await _personService.IsPhoneExists(Phone);

            if (ValidationStatus == enValidationStatus.NotExists)
                return Ok(new Response() { Success = true, Message = "Phone Is Valid" });

            return BadRequest(new Response() { Message = "Invalid Phone,This Phone Is Already Exists" });
        }
        [HttpPut("ProfileImage")]
        public async Task<IActionResult> UpdateProfileImageAsync([FromBody] AddImageRequest request)
        {
            var  roleStatus= await _userService.GetRoleByTokenAsync(request.Token);

            if (roleStatus == enPersonStatus.Unauthorized)
                return Unauthorized(new Response {Message = "Unauthorized Access"});

            var (_, imageStatus) = await _personService.UpdateImageAsync(request.DTO);

            return imageStatus switch
            {
                enUpdateImageStatus.Success =>
                    Ok(new Response
                    {
                        Success = true,
                        Message = "Image Updated Successfully"
                    }),

                enUpdateImageStatus.ImageNotFound =>
                    NotFound(new Response
                    {
                        Success = false,
                        Message = "Old Image Not Found"
                    }),

                enUpdateImageStatus.PersonNotFound =>
                    NotFound(new Response
                    {
                        Success = false,
                        Message = "Person Not Found"
                    }),

                enUpdateImageStatus.FailedToDeleteOldImage =>
                    BadRequest(new Response
                    {
                        Success = false,
                        Message = "Failed To Delete Old Image"
                    }),

                _ =>
                    BadRequest(new Response
                    {
                        Success = false,
                        Message = "Failed To Update Image"
                    })
            };
        }
    }
}
