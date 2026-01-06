namespace DVLD.DataAccessLayer.DTOs.License
{
    public class ReplaceLDLicenseDTO
    {
        public int LDLicenseID { get; set; }
        public int ReplacementType { get; set; }
        public DateTime ExpirationDate { get; set; }
        public string IssueReason { get; set; }
        public string Notes { get; set; }
        public int LicenseClassID { get; set; }
        public int LicenseTypeID { get; set; }
        public decimal PaidFees { get; set; }
        public int DriverID { get; set; }
        public int LDLApplicationID { get; set; }
    }
}
