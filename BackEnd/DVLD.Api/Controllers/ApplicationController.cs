using DVLD.BusinessLogicLayer;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Api.Controllers
{
    [Route("api/Application")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly ApplicationService _applicationService;
        public ApplicationController(IConfiguration configuration)
        {
            string conectionString = configuration.GetConnectionString("DefaultConnection");
            _applicationService = new ApplicationService(conectionString);
        }
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok("Hellow DVLD");
        }
    }
}
