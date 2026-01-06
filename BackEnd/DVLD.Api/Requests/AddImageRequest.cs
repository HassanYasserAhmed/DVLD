using DVLD.DataAccessLayer.DTOs.Person;
namespace DVLD.Api.Requests
{
    public class AddImageRequest
    {
        public string Token { get; set; }
        public ProfileImageDTO DTO { get; set; }
    }
}
