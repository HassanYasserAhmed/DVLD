using DVLD.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Api.Controllers
{
    [Route("api/Gendor")]
    [ApiController]
    public class GendorController : ControllerBase
    {
        private readonly GendorService _gendorService;
        public GendorController(IConfiguration configuration)
        {
            string conectionString = configuration.GetConnectionString("DefaultConnection");
            _gendorService = new GendorService(conectionString);
        }
    }
}
