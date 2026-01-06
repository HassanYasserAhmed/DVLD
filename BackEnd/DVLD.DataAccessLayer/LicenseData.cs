using DVLD.DataAccessLayer.DTOs.License;
using DVLD.DataAccessLayer.GeneralAdoMethods;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.DataAccessLayer
{
    public class LicenseData
    {
        private readonly string _connectionString;
        private readonly LogData _logData;
        public LicenseData(string connectionString)
        {
            _connectionString = connectionString;
            _logData = new LogData(connectionString);
        }
        public async Task<int> IssueLocalDrivingLicenseAsync(IssueLDLicenseDTO DTO) { return -1; }
        public async Task<int> IssueInternationalDrivingLicenseAsync(int LDLicenseID) { return -1; }
        public async Task<GetLocalDriverLicenseDTO> GetDriverLicenseByLDLApplicationIDAsync(int LDLApplicationID) {
            try
            {
                GetLocalDriverLicenseDTO LDLicense = null;

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select L.LicenseID,LC.LicenseClassID,
                                LC.LicenseClassName,P.FullName,P.NationalNumber,
                                G.GendorName,L.IssueDate,L.IssueReason,
                                L.Notes,L.IsActive,P.DateOfBirth,
                                D.DriverID,L.ExpirationDate,L.IsDetained
                                from Licenses as L
                                inner join LicenseClasses as LC on
                                LC.LicenseClassID=L.LicenseClassID
                                inner join Drivers as D on
                                D.DriverID=L.DriverID
                                inner join People as P on
                                P.PersonID=D.PersonID
                                inner join Gendors as G
                                on G.GendorID=P.GendorID
                                where L.LDLApplicationID=@LDLApplicationID";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@LDLApplicationID", SqlDbType.Int).Value = LDLApplicationID;

                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                if (await reader.ReadAsync())
                {
                    LDLicense = new GetLocalDriverLicenseDTO()
                    {
                        LicenseID = reader.GetInt32(reader.GetOrdinal("LicenseID")),
                        LicenseClassID = reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                        LicenseClassName = reader.GetString(reader.GetOrdinal("LicenseClassName")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        NationalNumber = reader.GetString(reader.GetOrdinal("NationalNumber")),
                        GendorName = reader.GetString(reader.GetOrdinal("GendorName")),
                        IssueDate = reader.GetDateOnly("IssueDate"),
                        IssueReason = reader.GetString(reader.GetOrdinal("IssueReason")),
                        Notes = reader.GetString(reader.GetOrdinal("Notes")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                        DateOfBirth = reader.GetDateOnly("DateOfBirth"),
                        DriverID = reader.GetInt32(reader.GetOrdinal("DriverID")),
                        ExpirationDate = reader.GetDateOnly("ExpirationDate"),
                        IsDetained = reader.GetBoolean(reader.GetOrdinal("IsDetained"))
                    };
                }
                return LDLicense;

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "LicenseData", "GetDriverLicenseByLDLApplicationIDAsync", ex.Message);
                return null;
            }
        }
        public async Task<bool> IsPersonHasApplicationWithTheSameLicenseClass(int PersonID,int LicenseClassID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

                string Query = @"select top(1) 1 from Applications as A
                                    inner join LocalDrivingLicenseApplications as LDLA
                                    on A.ApplicationID=LDLA.ApplicationID
                                    where LDLA.LicenseClassID=@LicenseClassID
                                    and A.ApplicationStatus !=2;"; //2 is mean Cancelled

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@LDLApplicationID", SqlDbType.Int).Value = PersonID;
                cmd.Parameters.Add("@LicenseClassID", SqlDbType.Int).Value = LicenseClassID;

                await conn.OpenAsync();

                object Result = await cmd.ExecuteScalarAsync();

                return (Result != null && Result != DBNull.Value);

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "LicenseData", "IsPersonHasApplicationWithTheSameLicenseClass", ex.Message);
                return false;
            }
        }
        public async Task<List<GetLocalDriverLicenseDTO>> GetAllLocalDriverLicensesAsync(int PersonID) {
            try
            {
                List<GetLocalDriverLicenseDTO> users = new List<GetLocalDriverLicenseDTO>();

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select  LC.LicenseClassID,LC.LicenseClassName,
                                    P.FullName,L.LicenseID,P.NationalNumber,
                                    G.GendorName,L.IsDetained,L.IssueReason,
                                    L.Notes,L.IsActive,P.DateOfBirth,D.DriverID,
                                    L.ExpirationDate,L.IsDetained from Licenses as L
                                    inner join LicenseClasses as LC
                                    on L.LicenseClassID=LC.LicenseClassID
                                    inner join Drivers as D on
                                    D.DriverID=L.DriverID
                                    inner join People as P on P.PersonID=D.PersonID
                                    inner join Gendors as G on P.GendorID=G.GendorID";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    users.Add(new GetLocalDriverLicenseDTO()
                    {
                        LicenseClassID = reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                        LicenseClassName = reader.GetString(reader.GetOrdinal("LicenseClassName")),
                        DateOfBirth = reader.GetDateOnly("DateOfBirth"),
                        LicenseID = reader.GetInt32(reader.GetOrdinal("LicenseID")),
                        NationalNumber = reader.GetString(reader.GetOrdinal("NationalNumber")),
                        GendorName = reader.GetString(reader.GetOrdinal("GendorName")),
                        IssueDate = reader.GetDateOnly("IssueDate"),
                        IssueReason = reader.GetString(reader.GetOrdinal("IssueReason")),
                        Notes = reader.GetString(reader.GetOrdinal("Notes")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                        DriverID = reader.GetInt32(reader.GetOrdinal("DriverID")),
                        ExpirationDate = reader.GetDateOnly("ExpirationDate"),
                        IsDetained = reader.GetBoolean(reader.GetOrdinal("IsDetained"))
                    });
                }

                return users;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "LicenseData", "GetAllLocalDriverLicensesAsync", ex.Message);
                return new List<GetLocalDriverLicenseDTO>();
            }
        }
        public async Task<List<GetInternationalDrivingLicensesDTO>> GetAllInternationalDrivingLicensesAsync(int PersonID) {

            try
            {
                List<GetInternationalDrivingLicensesDTO> IntLicenses = new List<GetInternationalDrivingLicensesDTO>();

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select LicenseID as LDLicenseID,IntL.IntLicenseID,IssueDate,IntL.ExpirationDate,
                                IssueReason,IsDetained,IntL.Notes,IntL.IsActive,
                                LC.LicenseClassName,LT.LicenceTypeName,
                                L.PaidFees,D.DriverID,A.ApplicationID,
                                P.GendorID,P.NationalNumber,p.FullName,
                                p.DateOfBirth
                                from Licenses as L
                                inner join LicenseClasses as LC
                                on L.LicenseClassID=LC.LicenseClassID
                                inner join LicenceTypes as LT on
                                L.LicenceTypeID=LT.LicenceTypeID
                                inner join Drivers as D on
                                D.DriverID=L.DriverID
                                inner join People as P
                                on P.PersonID=D.DriverID
                                inner join Applications as A on
                                A.PersonID=P.PersonID
                                inner join ApplicationTypes as At
                                on AT.ApplicationTypeID=A.ApplicationTypeID
                                inner join InternationalLicenses as IntL
                                on IntL.IntLicenseID=L.IntLicenseID
                                where P.PersonID=@PersonID";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@PersonID",SqlDbType.Int).Value= PersonID;
                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    IntLicenses.Add(new GetInternationalDrivingLicensesDTO()
                    {
                        LLicenseID = reader.GetInt32(reader.GetOrdinal("LDLicenseID")),
                        IntDLicenseID = reader.GetInt32(reader.GetOrdinal("IntLicenseID")),
                        ApplicationID = reader.GetInt32(reader.GetOrdinal("ApplicationID")),
                        DriverID = reader.GetInt32(reader.GetOrdinal("DriverID")),
                        IssueDate = reader.GetDateTime(reader.GetOrdinal("IssueDate")),
                        ExpirateionDate = reader.GetDateTime(reader.GetOrdinal("ExpirateionDate")),
                        IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        Gendor = reader.GetString(reader.GetOrdinal("Gendor")),
                        NationalNumber = reader.GetString(reader.GetOrdinal("NationalNumber")),
                        DateOfBirth = reader.GetDateOnly("DateOfBirth")
                    });
                }

                return IntLicenses;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "LicenseData", "GetAllInternationalDrivingLicensesAsync", ex.Message);
                return new List<GetInternationalDrivingLicensesDTO>();
            }
        }
        public async Task<int> RenewAsync(int LDLicenseID) { return -1; }
        public async Task<int> ReplaceAsync(ReplaceLDLicenseDTO DTO) { return -1; }
        public async Task<int> DetainAsync(DetainLDLicenseDTO DTO) {
            try
            {
                int NewID = -1;
                using SqlConnection conn = new SqlConnection(_connectionString);

                string Query = @"insert into DetainLicenses(RLApplicationID,FineFees)
                                values (@LDLApplicationID,@FineFees);
                                select SCOPE_IDENTITY();";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@LDLApplicationID", SqlDbType.Int).Value = DTO.LDLicenseID;
                cmd.Parameters.Add("@FineFees", SqlDbType.Decimal).Value = DTO.FineFees;

                await conn.OpenAsync();

                object Result = await cmd.ExecuteScalarAsync();
                if (Result != null && Result != DBNull.Value)
                    NewID = Convert.ToInt32((decimal)Result);

                return NewID;

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "LicenseData", "DetainAsync", ex.Message);
                return -1;
            }
        }
        public async Task<bool> ReleaseAsync(int LDLicenseDTO) {
           try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

                string Query = @"update DetainLicenses
                                set IsReleased=1,
	                                ReleaseDate=GETDATE()
                                where RLApplicationID=@LDLApplicationID
	                                  and IsReleased=0;";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@LDLApplicationID", SqlDbType.Int).Value = LDLicenseDTO;

                await conn.OpenAsync();

                int RowsAffected = await cmd.ExecuteNonQueryAsync();

                return RowsAffected > 0;
            }
            catch (Exception ex)
           {
                await _logData.AddNewLogAsync("DAL", "LicenseData", "ReleaseAsync", ex.Message);
                return false;
           }
        }
        public async Task<List<LicenseClassDTO>> GetAllLisenceClassesAsync() {
            try
            {
                List<LicenseClassDTO> licensesClasses = new List<LicenseClassDTO>();

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select LicenseClassID,LicenseClassName,ValidationYears
                                    from LicenseClasses";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    licensesClasses.Add(new LicenseClassDTO()
                    {
                        LicenseClassID = reader.GetInt32(reader.GetOrdinal("LicenseClassID")),
                        LicenseClassName = reader.GetString(reader.GetOrdinal("LicenseClassName")),
                        ValidationYears = reader.GetInt32(reader.GetOrdinal("ValidationYears")),
                    });
                }

                return licensesClasses;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "LicenseData", "GetAllLisenceClassesAsync", ex.Message);
                return new List<LicenseClassDTO>();
            }
        }
    }
}