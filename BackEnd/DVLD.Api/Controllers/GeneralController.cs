using DVLD.BusinessLogicLayer;
using DVLD.BusinessLogicLayer.Enums;
using DVLD.DataAccessLayer.DTOs.General;
using DVLD.Api.Responses;
using Microsoft.AspNetCore.Mvc;

namespace DVLD.Api.Controllers
{
    [Route("api/General")]
    [ApiController]
    public class GeneralController : ControllerBase
    {
        private readonly CountryService _countryService;
        public GeneralController(IConfiguration configuration)
        {
            string conectionString = configuration.GetConnectionString("DefaultConnection");
            _countryService = new CountryService(conectionString);
        }
        [HttpGet("Countries")]
        public async Task<ActionResult> GetAllCountriesAsync()
        {
            (List<CountryDTO> countries, enCountry CountryStatus) = await _countryService.GetAllAsync();

            if(CountryStatus == enCountry.Found)
                return Ok(new Response() { Success=true,Message="Countries Found Successfully",Data=countries});

            return NotFound(new Response() { Message = "Countries Not Found" });

        }
    }
}
