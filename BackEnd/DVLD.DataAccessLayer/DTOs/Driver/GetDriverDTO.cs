namespace DVLD.DataAccessLayer.DTOs.Driver
{
    public class GetDriverDTO
    {
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public string NationalNumber { get; set; }
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; }
        public int ActiveLicenses { get; set; } = 0;
    }
}
