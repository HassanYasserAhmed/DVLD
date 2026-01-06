using DVLD.BusinessLogicLayer.Enums;
using DVLD.DataAccessLayer;
using DVLD.DataAccessLayer.DTOs.Application;
using DVLD.DataAccessLayer.DTOs.License;
namespace DVLD.BusinessLogicLayer
{
    public class LicenseService
    {
        private readonly LicenseData _licenseData;
        private readonly ApplicationService _applicationService;
        public LicenseService(string connectionString)
        {
            _licenseData = new LicenseData(connectionString);
            _applicationService = new ApplicationService(connectionString);
        }
        // just skep errors

        //public async Task<(int NewLDLicense,enLicense LicenseStatus)> IssueLocalDrivingLicenseAsync(int LDLApplicationID,int CreatedBy) {
        //    // Get LDLApplication and check if its complited
        //  (LDLApplicationDTO LdlApplication,enApplication  ApplicationStatus) =await _applicationService.GetLDLApplicationAsync(LDLApplicationID);

        //    if (ApplicationStatus == enApplication.NotFound)
        //        return (-1, enLicense.ApplicationForLicenseNotFound);

        //    if (LdlApplication.Status != (int)enApplicationStatus.Cancelled)
        //        return (-1, enLicense.ApplicationIsCancelled);
        //    if (LdlApplication.Status != (int)enApplicationStatus.Complited)
        //        return (-1, enLicense.ApplicationIsComplited);
        //    List<LicenseClassDTO> licenseClasses = await _licenseData.GetAllLisenceClassesAsync();
        //    LicenseClassDTO licenseClass = null; ;

        //    foreach(LicenseClassDTO licenseClassDTO in licenseClasses)
        //    {
        //        if (licenseClassDTO.LicenseClassName == LdlApplication.LicenseClassName)
        //            licenseClass = licenseClassDTO;
        //    }

        //    if (licenseClasses.Count == 0)
        //        return (-1, enLicense.Failed);
        //    DateTime now = new DateTime();

        //    IssueLDLicenseDTO issueLDLicenseDTO = new IssueLDLicenseDTO()
        //    {
        //   IssueDate = DateTime.UtcNow, 
        //  ExpireationDate =now.AddYears(licenseClass.ValidationYeasers),
        // IssueReason=LdlApplication.iss,
        //  Notes ,
        //  LicenseClassID, 
        //  LicenseTypeID ,
        //  PaidFees ,
        //  DriverID,
        //  LDLApplicationID 
        //    }
        //    int NewLDLicenseID = await _licenseData.IssueLocalDrivingLicenseAsync()

        //    return (-1, enLicense.Failed);
            
        //}
        public async Task<int> IssueInternationalDrivingLicenseAsync(int LDLicenseID) { return -1; }
        public async Task<GetLocalDriverLicenseDTO> GetDriverLicenseByLDLApplicationIDAsync(int LDLApplicationID) { return new GetLocalDriverLicenseDTO(); }
        public async Task<List<GetLocalDriverLicenseDTO>> GetAllLocalDriverLicensesAsync(int PersonID) { return new List<GetLocalDriverLicenseDTO>(); }
        public async Task<List<GetInternationalDrivingLicensesDTO>> GetAllInternationalDrivingLicensesAsync(int PersonID) { return new List<GetInternationalDrivingLicensesDTO>(); }
        public async Task<int> RenewAsync(int LDLicenseID) { return -1; }
        public async Task<int> ReplaceAsync(ReplaceLDLicenseDTO DTO) { return -1; }
        public async Task<int> DetainAsync(DetainLDLicenseDTO DTO) { return -1; }
        public async Task<bool> ReleaseAsync(int LDLicenseDTO)  { return false; }
    }
}