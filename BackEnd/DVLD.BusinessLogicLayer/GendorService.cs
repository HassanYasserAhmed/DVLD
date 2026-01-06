using DVLD.BusinessLogicLayer.Enums;
using DVLD.DataAccessLayer;
using DVLD.DataAccessLayer.DTOs.General;

namespace DVLD.BusinessLogicLayer
{
    public class GendorService
    {
        private readonly GendorData _gendorData;
        public GendorService(string connectionString)
        {
            _gendorData = new GendorData(connectionString);
        }
        public async Task<(List<GendorDTO> Gendors ,enGendor GendorStatus)> GetAllAsync()
        {
            List<GendorDTO> gendors = await _gendorData.GetAllAsync();
            if (gendors.Count == 0)
                return (new List<GendorDTO>(),enGendor.NotFound);
            else
                return (gendors,enGendor.Found);
        }
    }
}
