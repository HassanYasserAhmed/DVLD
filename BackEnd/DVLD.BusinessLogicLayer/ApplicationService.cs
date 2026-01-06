using DVLD.BusinessLogicLayer.Enums;
using DVLD.DataAccessLayer;
using DVLD.DataAccessLayer.DTOs.Application;
namespace DVLD.BusinessLogicLayer
{
    public class ApplicationService
    {
        private readonly ApplicationData _applicationData;
        private readonly LicenseData _licenseData;
        public ApplicationService(string connectionString)
        {
            _applicationData = new ApplicationData(connectionString);
            _licenseData = new LicenseData(connectionString);
        }

        public async Task<(List<ApplicationTypeDTO> ApplicationTypes,enApplication ApplicationStatus)> GetAllApplicationTypesAsync()  { 
        List<ApplicationTypeDTO> applicationTypes = await _applicationData.GetAllApplicationTypesAsync();
            if (applicationTypes.Count == 0)
                return (applicationTypes, enApplication.NotFound);
            else
                return (applicationTypes, enApplication.Found);
        }
        public async Task<enApplication> UpdateApplicationTypeAsync(UpdateApplicationTypeDTO DTO)  {
        bool IsUpdated = await _applicationData.UpdateApplicationTypeAsync(DTO);

            if (IsUpdated)
                return enApplication.Success;
            else
                return enApplication.Failed;
        }

        public async Task<(List<LDLApplicationDTO> LDLApplications,enApplication ApplicationStatus)> GetAllLDLApplicationsAsync() {
            List<LDLApplicationDTO> lDLApplication = await _applicationData.GetAllLDLApplicationsAsync();

            if (lDLApplication.Count == 0)
                return (lDLApplication, enApplication.NotFound);
            else 
                return (lDLApplication, enApplication.Found);
        }
        public async Task<(LDLApplicationDTO ldlApplication,enApplication applicationStatus)> GetLDLApplicationAsync(int LDLApplicationID) { 
        LDLApplicationDTO ldlApplication = await _applicationData.GetLDLApplicationAsync(LDLApplicationID);

            if (ldlApplication == null)
                return (null,enApplication.NotFound);
            else 
                return (ldlApplication,enApplication.Found);
        }
        public async Task<(int NewLDLApplicationID,enApplication ApplicationStatus)> AddNewLDApplicationAsync(AddNewLDLApplicationDTO DTO)  {

            bool IsPersonHas = await _licenseData.IsPersonHasApplicationWithTheSameLicenseClass(DTO.PersonID, DTO.LicenseClassID);

            if (IsPersonHas)
                return (-1, enApplication.HasOneWithTheSameLicenseClass);

            int NewID = await _applicationData.AddNewLDApplicationAsync(DTO);

            if (NewID > 0)
                return (NewID, enApplication.Success);
            else
                return (NewID, enApplication.Failed);
        }
        public async Task<enApplication> DeleteAsync(int ApplicationID) { 
            bool IsDeleted = await _applicationData.DeleteAsync(ApplicationID);
            if (IsDeleted)
                return enApplication.Success;
            else
                return enApplication.Failed;
        }
        public async Task<enApplication> ChangeStatus(ChangeApplicationStatusDTO DTO)  {
            bool IsChanged = await _applicationData.ChangeStatus(DTO);
            if(IsChanged)
                return enApplication.Success;
            else
                return enApplication.Failed;
        }
    }
}
