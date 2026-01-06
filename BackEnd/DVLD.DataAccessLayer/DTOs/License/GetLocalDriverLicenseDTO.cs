namespace DVLD.DataAccessLayer.DTOs.License
{
    public class GetLocalDriverLicenseDTO
    {

        public int LicenseID { get; set; }
        public int LicenseClassID { get; set; }
        public string LicenseClassName { get; set; }
        public string FullName { get; set; }
        public string NationalNumber { get; set; }
        public string GendorName { get; set; }
        public DateOnly IssueDate { get; set; }
        public string IssueReason { get; set; }
        public string Notes { get; set; }
        public bool IsActive { get; set; }
        public DateOnly DateOfBirth { get; set; }
        public int DriverID { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public bool IsDetained { get; set; }
    }
}
