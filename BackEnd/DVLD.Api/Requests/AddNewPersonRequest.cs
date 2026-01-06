using DVLD.DataAccessLayer.DTOs.Person;
namespace DVLD.Api.Requests
{
    public class AddNewPersonRequest
    {
        public string Token { get; set; }
        public AddNewPersonDTO PersonData { get; set; }
        public IFormFile ImageData { get; set; }
    }
}
