using DVLD.DataAccessLayer.DTOs.Application;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.DataAccessLayer
{
    public class ApplicationData
    {
        private readonly string _connectionString;
        private readonly LogData _logData;
        public ApplicationData(string connectionString) 
        {
            _connectionString = connectionString;
            _logData = new LogData(connectionString);
        }
        public async Task<List<ApplicationTypeDTO>> GetAllApplicationTypesAsync() {

            try
            {
                List<ApplicationTypeDTO> applicationTypes = new List<ApplicationTypeDTO>();

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select ApplicationTypeID,ApplicationTypeName,ApplicationFees
                                from ApplicationTypes;";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    applicationTypes.Add(new ApplicationTypeDTO()
                    {
                        ApplicationTypeID = reader.GetInt32(reader.GetOrdinal("ApplicationTypeID")),
                        ApplicationTypeName = reader.GetString(reader.GetOrdinal("ApplicationTypeName")),
                        ApplicationTypeFees = reader.GetDecimal(reader.GetOrdinal("ApplicationFees")),
                    });
                }

                return applicationTypes;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "ApplicationData", "GetAllApplicationTypesAsync", ex.Message);
                return new List<ApplicationTypeDTO>();
            }
        }
        public async Task<bool> UpdateApplicationTypeAsync(UpdateApplicationTypeDTO DTO ) {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

                string Query = @"update ApplicationTypes
                                set ApplicationTypeName=@NewApplicationTypeName,
	                                ApplicationFees=@NewApplicationFees
                                where ApplicationTypeID=@ApplicationTypeID;";
                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@ApplicationTypeID", SqlDbType.NVarChar, 50).Value = DTO.ApplicationTypeID;
                cmd.Parameters.Add("@NewApplicationTypeName", SqlDbType.Int).Value = DTO.ApplicationName;
                cmd.Parameters.Add("@NewApplicationFees", SqlDbType.DateTime).Value = DTO.ApplicationTypeFees;

                await conn.OpenAsync();

                int RowsAffected = await cmd.ExecuteNonQueryAsync();

                return RowsAffected > 0;

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "ApplicationData", "UpdateApplicationTypeAsync", ex.Message);
                return false;
            }
        }

        public async Task<List<LDLApplicationDTO>> GetAllLDLApplicationsAsync() {

            try
            {
                List<LDLApplicationDTO> LDLApplications = new List<LDLApplicationDTO>();

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select L.LDLApplicationID,Lc.LicenseClassName,P.PersonID,
                                    P.NationalNumber,p.FullName,A.ApplicationDate,
                                    count(CASE WHEN T.TestResult = 1 THEN 1 ELSE 0 END) as PassedExames,
                                    A.ApplicationStatus from Applications as A
                                    inner join People as P
	                                    on P.PersonID=A.PersonID
                                    inner join LocalDrivingLicenseApplications as L
                                    on L.ApplicationID=A.ApplicationID
                                    inner join LicenseClasses as Lc
                                    on Lc.LicenseClassID=L.LicenseClassID
                                    inner join TestAppointments as TA
                                    on TA.LDLApplicationID=L.LDLApplicationID
                                    inner join Tests as T
                                    on T.TestAppointmentID=TA.TestAppointmentID
                                    where A.IsDeleted=0
                                    GROUP BY
                                        L.LDLApplicationID,
                                        Lc.LicenseClassName,
                                        P.NationalNumber,
                                        P.FullName,
                                        A.ApplicationDate,
                                        A.ApplicationStatus;";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    LDLApplications.Add(new LDLApplicationDTO()
                    {
                        LDLApplicationID = reader.GetInt32(reader.GetOrdinal("LDLApplicationID")),
                        LicenseClassName = reader.GetString(reader.GetOrdinal("LicenseClassName")),
                        NationalNumber = reader.GetString(reader.GetOrdinal("NationalNumber")),
                        PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        ApplicationDate = reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                        Status = reader.GetInt32(reader.GetOrdinal("ApplicationStatus"))
                    });
                }
                return LDLApplications;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "ApplicationData", "GetAllLDLApplicationsAsync", ex.Message);
                return new List<LDLApplicationDTO>();
            }
        }
        public async Task<LDLApplicationDTO> GetLDLApplicationAsync(int LDLApplicationID) {
            try
            {
                LDLApplicationDTO LDLApplication = null;

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select L.LDLApplicationID,Lc.LicenseClassName,
                                    ,P.PersonID,P.NationalNumber,p.FullName,A.ApplicationDate,
                                    count(CASE WHEN T.TestResult = 1 THEN 1 ELSE 0 END) as PassedExames,
                                    A.ApplicationStatus from Applications as A
                                    inner join People as P
	                                    on P.PersonID=A.PersonID
                                    inner join LocalDrivingLicenseApplications as L
                                    on L.ApplicationID=A.ApplicationID
                                    inner join LicenseClasses as Lc
                                    on Lc.LicenseClassID=L.LicenseClassID
                                    inner join TestAppointments as TA
                                    on TA.LDLApplicationID=L.LDLApplicationID
                                    inner join Tests as T
                                    on T.TestAppointmentID=TA.TestAppointmentID
                                    where L.LDLApplicationID=@LDLApplicationID and A.IsDeleted=0
                                    GROUP BY
                                        L.LDLApplicationID,
                                        Lc.LicenseClassName,
                                        P.NationalNumber,
                                        P.FullName,
                                        A.ApplicationDate,
                                        A.ApplicationStatus;";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@LDLApplicationID", SqlDbType.Int).Value =LDLApplicationID;

                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    LDLApplication = new LDLApplicationDTO()
                    {
                        LDLApplicationID = reader.GetInt32(reader.GetOrdinal("LDLApplicationID")),
                        LicenseClassName = reader.GetString(reader.GetOrdinal("LicenseClassName")),
                        NationalNumber = reader.GetString(reader.GetOrdinal("NationalNumber")),
                        PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        ApplicationDate = reader.GetDateTime(reader.GetOrdinal("ApplicationDate")),
                        Status = reader.GetInt32(reader.GetOrdinal("ApplicationStatus"))
                    };
                }
                return LDLApplication;

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "ApplicationData", "GetLDLApplicationAsync", ex.Message);
                return null;
            }
        }
        public async Task<int> AddNewLDApplicationAsync(AddNewLDLApplicationDTO DTO) {
            try
            {

                int NewID = -1;
                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"BEGIN TRY
                                    BEGIN TRANSACTION;
	                                declare @ApplicationID int;
	                                insert into Applications(ApplicationFees,PersonID,ApplicationTypeID,CreatedBy)
	                                values (@ApplicationFees,@PersonID,@ApplicationTypeID,@CreatedBy);

	                                set @ApplicationID = SCOPE_IDENTITY();

	                                insert into LocalDrivingLicenseApplications(ApplicationID,LicenseClassID)
	                                values(@ApplicationID,@LicenseClass);

	                                select SCOPE_IDENTITY();
                                    COMMIT TRANSACTION;
                                END TRY
                                BEGIN CATCH
                                    ROLLBACK TRANSACTION;
                                    SELECT 
                                        ERROR_NUMBER() AS ErrorNumber,
                                        ERROR_MESSAGE() AS ErrorMessage;
                                END CATCH;";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@ApplicationFees", SqlDbType.Decimal).Value = DTO.ApplicationFees;
                cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = DTO.PersonID;
                cmd.Parameters.Add("@ApplicationTypeID", SqlDbType.Int).Value = DTO.ApplicationType;
                cmd.Parameters.Add("@CreatedBy", SqlDbType.Int).Value = DTO.CreatedBy;
                cmd.Parameters.Add("@LicenseClass", SqlDbType.Int).Value = DTO.LicenseClassID;
                await conn.OpenAsync();
                object Result = await cmd.ExecuteScalarAsync();

                if (Result != null && Result != DBNull.Value)
                    NewID = Convert.ToInt32((decimal)Result);
                return NewID;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "ApplicationData", "AddNewLDLApplicationAsync", ex.Message);
                return -1;
            }
        }
        public async Task<bool> DeleteAsync(int ApplicationID) {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

                string Query = @"update Applications
                                    set isDeleted=1
                                where ApplicationID=@ApplicationID
                                      and IsDeleted=0;";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = ApplicationID;

                await conn.OpenAsync();

                int RowsAffected = await cmd.ExecuteNonQueryAsync();

                return RowsAffected > 0;

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "ApplicationData", "DeleteAsync", ex.Message);
                return false;
            }
        }
        public async Task<bool> ChangeStatus (ChangeApplicationStatusDTO DTO) {

            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

                string Query = @"update Applications
                                   set ApplicationStatus=@NewStatus
                                where ApplicationID= @ApplicationID
	                                 and isDeleted=0;";

                using SqlCommand cmd = new SqlCommand(Query, conn);

                cmd.Parameters.Add("@ApplicationID", SqlDbType.Int).Value = DTO.ApplicationId;
                cmd.Parameters.Add("@NewStatus", SqlDbType.NVarChar, 50).Value = DTO.NewStatus;

                await conn.OpenAsync();

                int RowsAffected = await cmd.ExecuteNonQueryAsync();

                return RowsAffected > 0;

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "ApplicationData", "ChangeStatusAsync", ex.Message);
                return false;
            }
        }
    }
}
