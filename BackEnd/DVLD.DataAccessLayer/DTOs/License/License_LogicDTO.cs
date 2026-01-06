namespace DVLD.DataAccessLayer.DTOs.License
{
    public class License_LogicDTO
    {
        public int LicenseID { get; set; }
        public bool IsDetained { get; set; }
        public bool IsActive { get; set; }
        public int LicenseClassID { get; set; }
        public int LicenseTypeID { get; set; }
        public int DriverID { get; set; }
        public int LDLicenseID { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
