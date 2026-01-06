using DVLD.BusinessLogicLayer.Enums;
using DVLD.DataAccessLayer;
using DVLD.DataAccessLayer.DTOs.General;

namespace DVLD.BusinessLogicLayer
{
    public class CountryService
    {
        private readonly CountryData _countryData;
        public CountryService(string connectionString)
        {
            _countryData = new CountryData(connectionString);
        }
        public async Task<(List<CountryDTO> countries,enCountry CountryStatus)> GetAllAsync()
        {
            List<CountryDTO> countries = await _countryData.GetAllAsync();
            if (countries.Count == 0)
                return (countries, enCountry.NotFound);
            else
                return (countries,enCountry.Found);
        }
    }
}
