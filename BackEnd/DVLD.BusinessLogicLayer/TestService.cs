using DVLD.BusinessLogicLayer.Enums;
using DVLD.DataAccessLayer.DTOs.Test;
namespace DVLD.BusinessLogicLayer
{
    public class TestService
    {
        private readonly DataAccessLayer.TestData _testData;
            public TestService(string ConnectionString)
        {
            _testData = new DataAccessLayer.TestData(ConnectionString);
        }
        public async Task<(List<TestAppointmentDTO> TestAppointments,enTest TestStatus)> GetAllTestAppointmentsByIDAsync(GetAllTestAppointmentsByIDDTO DTO)  {
            List<TestAppointmentDTO> testAppointments = await _testData.GetAllTestAppointmentsByIDAsync(DTO);
            if (testAppointments.Count == 0)
                return (new List<TestAppointmentDTO>(), enTest.AppointmentsNotFound);
            else
                return (testAppointments, enTest.AppointmentsFound);
        }
        public async Task<(int NewTestID,enTest TestStatus)> AddTestAppointmentAsync(AddTestAppointmentDTO DTO)  {

            int PassedTests = await _testData.GetCountOfPassedTestsAsync(DTO.LDLApplicationID);

            int NextTestType = PassedTests switch
            {
                0 => (int)enTestTypes.VisionTest,
                1 => (int)enTestTypes.WrittenTest,
                2 => (int)enTestTypes.StreetTest,
                _ => -1
            };

            if (NextTestType == -1)
                return (-1, enTest.Failed);

            int NewTestID = await _testData.AddTestAppointmentAsync(DTO,NextTestType);

            if(NewTestID > 0)
                return (NewTestID, enTest.Success);
            else
                return (-1,enTest.Failed);
        }
        public async Task<enTest> ChangeTestAppointmentDate(ChangeTestAppointmentDateDTO DTO)  {
            bool IsChanged = await _testData.ChangeTestAppointmentDate(DTO);
            if (IsChanged)
                return enTest.Success;
            else
                return enTest.Failed;
        }
        public async Task<int> TakTastAsync(TakeTestDTO DTO)  { return -1; }
        public async Task<(List<TestTypeDTO> testTypes,enTest TestStatus)> GetAllTestTypesAsync() {
            List<TestTypeDTO> testTypes = await _testData.GetAllTestTypesAsync();
            if (testTypes.Count == 0)
                return (new List<TestTypeDTO>(), enTest.NotFound);
            else
                return (testTypes, enTest.Success);
        }
        public async Task<enTest> UpdateTestTypeAsync(UpdateTestTypeDTO DTO) {
            bool IsUpdated = await _testData.UpdateTestTypeAsync(DTO);
            if(IsUpdated) return enTest.Success;
            else return enTest.Failed;
        }
    }
}
