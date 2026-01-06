using DVLD.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Api.Controllers
{
    [Route("api/Driver")]
    [ApiController]
    public class DriverController : ControllerBase
    {
        private readonly DriverService _driverService;
        public DriverController(IConfiguration configuration)
        {
            string conectionString = configuration.GetConnectionString("DefaultConnection");
            _driverService = new DriverService(conectionString);
        }
    }
}
