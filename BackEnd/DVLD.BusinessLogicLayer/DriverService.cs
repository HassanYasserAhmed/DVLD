using DVLD.BusinessLogicLayer.Enums;
using DVLD.DataAccessLayer;
using DVLD.DataAccessLayer.DTOs.Driver;

namespace DVLD.BusinessLogicLayer
{
    public class DriverService
    {
        private readonly DriverData _driverData;
        public DriverService(string connectionString)
        {
            _driverData = new DriverData(connectionString);
        }
        public async Task<(List<GetDriverDTO> Drivers,enDriver DriverStatus)> GetAllAsync()
        {
            List<GetDriverDTO> drivers = await _driverData.GetAllAsync();

            if (drivers.Count == 0)
                return (drivers, enDriver.NotFound);
            else
                return (drivers, enDriver.Found);
        }
    }
}
