using DVLD.DataAccessLayer.DTOs;
namespace DVLD.Api.Requests
{
    public class UpdatePersonRequest
    {
        public string Token { get; set; }
        public UpdatePersonDTO PersonData { get; set; }
    }
}
