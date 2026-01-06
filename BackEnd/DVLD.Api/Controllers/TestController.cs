using DVLD.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Api.Controllers
{
    [Route("api/Test")]
    [ApiController]
    public class TestController : ControllerBase
    {
        private readonly TestService _testAppointmentService;
        public TestController(IConfiguration configuration)
        {
            string conectionString = configuration.GetConnectionString("DefaultConnection");
            _testAppointmentService = new TestService(conectionString);
        }
    }
}
