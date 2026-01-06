namespace DVLD.DataAccessLayer.DTOs.Application
{
    public class LDLApplicationDTO
    {
        public int LDLApplicationID { get; set; }
        public string LicenseClassName { get; set; }
        public string NationalNumber { get; set; }
        public int PersonID { get; set; }
        public string FullName { get; set; }
        public DateTime ApplicationDate     { get; set; }
        public int PassedTests { get; set; }
        public int Status { get; set; } // Only label In Front End
    }
}
