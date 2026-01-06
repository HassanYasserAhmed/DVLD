namespace DVLD.DataAccessLayer.DTOs.License
{
    public class IssueLDLicenseDTO
    {
        public DateTime IssueDate { get; set; }
        public DateTime ExpireationDate { get; set; }
        public int IssueReason { get; set; }
        public string Notes { get; set; } = string.Empty;
        public int LicenseClassID { get; set; }
        public int LicenseTypeID { get; set; }
        public decimal PaidFees { get; set; }
        public int DriverID { get; set; }
        public int LDLApplicationID { get; set; }
    }
}
