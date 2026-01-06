using DVLD.DataAccessLayer.DTOs.Driver;
using Microsoft.Data.SqlClient;

namespace DVLD.DataAccessLayer
{
    public class DriverData
    {
        private readonly string _connectionString;
        private readonly LogData _logData;
        public DriverData(string connectionString)
        {
            _connectionString = connectionString;
            _logData = new LogData(connectionString);
        }
        public async Task<List<GetDriverDTO>> GetAllAsync() {
            try
            {
                List<GetDriverDTO> drivers = new List<GetDriverDTO>();

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"SELECT 
                                    D.DriverID,P.PersonID, P.NationalNumber,P.FullName,D.CreatedAt,
                                    COUNT(CASE WHEN L.IsActive = 1 THEN 1 END) AS ActiveLicenses
                                FROM Drivers AS D
                                INNER JOIN People AS P
                                    ON D.PersonID = P.PersonID
                                LEFT JOIN Licenses AS L
                                    ON D.DriverID = L.DriverID
                                GROUP BY 
                                    D.DriverID,P.PersonID,P.NationalNumber,P.FullName,D.CreatedAt;";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    drivers.Add(new GetDriverDTO()
                    {
                        DriverID = reader.GetInt32(reader.GetOrdinal("DriverID")),
                        PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                        NationalNumber = reader.GetString(reader.GetOrdinal("NationalNumber")),
                        FullName = reader.GetString(reader.GetOrdinal("FullName")),
                        CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                        ActiveLicenses= reader.GetInt32(reader.GetOrdinal("ActiveLicenses"))
                    });
                }

                return drivers;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "DriverData", "GetAllAsync", ex.Message);
                return null;
            }
        }
    }
}
