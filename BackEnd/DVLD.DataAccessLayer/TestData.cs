using DVLD.DataAccessLayer.DTOs.Test;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.DataAccessLayer
{
    public class TestData
    {
        private readonly string _connectionString;
        private readonly LogData _logData;
        public TestData(string connectionString)
        {
            _connectionString = connectionString;
            _logData = new LogData(connectionString);
        }
        public async Task<List<TestAppointmentDTO>> GetAllTestAppointmentsByIDAsync(GetAllTestAppointmentsByIDDTO DTO) {
            try
            {
                List<TestAppointmentDTO> testAppointments = new List<TestAppointmentDTO>();

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select TestAppointmentID,TestAppointmentDate,
                                PaidFees,isLocked
                                from TestAppointments
                                where LDLApplicationID=@LDApplicationID
	                                  and TestTypeID=@TestTypeID";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    testAppointments.Add(new TestAppointmentDTO()
                    {
                        AppointmentID = reader.GetInt32(reader.GetOrdinal("TestAppointmentID")),
                        AppointmentDate = reader.GetDateTime(reader.GetOrdinal("TestAppointmentDate")),
                        PaidFees = reader.GetDecimal(reader.GetOrdinal("PaidFees")),
                        IsLocked = reader.GetBoolean(reader.GetOrdinal("isLocked"))
                    });
                }

                return testAppointments;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "", "", ex.Message);
                return new List<TestAppointmentDTO>();
            }
        }
        public async Task<int> AddTestAppointmentAsync(AddTestAppointmentDTO DTO,int NextTestType) {
            try
            {
                int NewID = -1;
                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"insert into TestAppointments
		                            (LDLApplicationID,
		                            TestAppointmentDate,
		                            TestTypeID)
                            values(@LDApplicationID,
	                               @TestAppointmentDate,
	                               @TestTypeID);

                            select SCOPE_IDENTITY();";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@LDApplicationID", SqlDbType.Int).Value = DTO.LDLApplicationID;
                cmd.Parameters.Add("@TestAppointmentDate", SqlDbType.Int).Value = DTO.TestAppointmentDate;
                cmd.Parameters.Add("@TestTypeID", SqlDbType.Int).Value = NextTestType;
                await conn.OpenAsync();
                object Result = await cmd.ExecuteScalarAsync();

                if (Result != null && Result != DBNull.Value)
                    NewID = Convert.ToInt32((decimal)Result);
                return NewID;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "TestData", "AddTestAppointmentAsync", ex.Message);
                return -1;
            }
        }
        public async Task<bool> ChangeTestAppointmentDate(ChangeTestAppointmentDateDTO DTO) {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

                string Query = @"update TestAppointments
                                    set TestAppointmentDate= @NewTestAppointmentDate
                                where TestAppointmentID=@TestAppointmentID
                                      and isLocked=0";
                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@NewTestAppointmentDate", SqlDbType.NVarChar, 50).Value = DTO.NewTestAppointmentDate;
                cmd.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = DTO.TestAppointmentID;
                await conn.OpenAsync();

                int RowsAffected = await cmd.ExecuteNonQueryAsync();

                return RowsAffected > 0;

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "TestData", "ChangeTestAppointmentDate", ex.Message);
                return false;
            }
        }
        public async Task<int> TakTastAsync(TakeTestDTO DTO) {
            try
            {
                int NewID = -1;
                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"insert into Tests(TestAppointmentID,TestResult,Notes)
                                     values(@TestAppointmentID,@TestResult,@Notes);
                                select SCOPE_IDENTITY();";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@TestAppointmentID", SqlDbType.Int).Value = DTO.TestAppointmentID;
                cmd.Parameters.Add("@TestResult", SqlDbType.Int).Value = DTO.IsPassed;
                cmd.Parameters.Add("@Notes", SqlDbType.DateTime).Value = DTO.Notes;
                await conn.OpenAsync();
                object Result = await cmd.ExecuteScalarAsync();

                if (Result != null && Result != DBNull.Value)
                    NewID = Convert.ToInt32((decimal)Result);
                return NewID;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "", "", ex.Message);
                return -1;
            }
        }
        public async Task<List<TestTypeDTO>> GetAllTestTypesAsync() {

            try
            {
                List<TestTypeDTO> tests = new List<TestTypeDTO>();

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select TesttypeID,TestTypeName,Description,Fees from TestTypes;";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    tests.Add(new TestTypeDTO()
                    {
                        TestTypeID = reader.GetInt32(reader.GetOrdinal("TesttypeID")),
                        TestTypeName = reader.GetString(reader.GetOrdinal("TestTypeName")),
                        Description = reader.GetString(reader.GetOrdinal("Description")),
                        Fees = reader.GetDecimal(reader.GetOrdinal("Fees"))
                    });
                }

                return tests;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "TestData", "GetAllTestTypesAsync", ex.Message);
                return new List<TestTypeDTO>();
            }
        }
        public async Task<bool> UpdateTestTypeAsync(UpdateTestTypeDTO DTO) {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

                string Query = @"update TestTypes
                                set TestTypeName=@NewTestTypeName,
	                                Description=@NewDescription,
	                                Fees = @NewFees
                                where TestTypeID=@TestTypeID";
                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@TestTypeID", SqlDbType.NVarChar, 50).Value = DTO.TestTypeID;
                cmd.Parameters.Add("@NewTestTypeName", SqlDbType.Int).Value = DTO.TestTypeName;
                cmd.Parameters.Add("@NewDescription", SqlDbType.DateTime).Value = DTO.Description;

                await conn.OpenAsync();

                int RowsAffected = await cmd.ExecuteNonQueryAsync();

                return RowsAffected > 0;

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "TestData", "UpdateTestTypeAsync", ex.Message);
                return false;
            }
        }
        public async Task<int> GetCountOfPassedTestsAsync(int LDLApplicationID)
        {
            try
            {

                int NumbersOfTests = 0;
                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select Count(*) from TestAppointments as TA
                                inner join Tests as T 
	                                on TA.TestAppointmentID=T.TestAppointmentID
                                where T.TestResult=1
	                                and TA.LDLApplicationID=@LDLApplicationID";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@LDLApplicationID", SqlDbType.Int).Value = LDLApplicationID;
                await conn.OpenAsync();
                object Result = await cmd.ExecuteScalarAsync();

                if (Result != null && Result != DBNull.Value)
                    NumbersOfTests = Convert.ToInt32((decimal)Result);
                return NumbersOfTests;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "TestData", "GetCountOfPassedTestsAsync", ex.Message);
                return 0;
            }
        }
    }
}
