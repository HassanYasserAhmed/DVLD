using DVLD.DataAccessLayer.DTOs.General;
using Microsoft.Data.SqlClient;

namespace DVLD.DataAccessLayer
{
    public class CountryData
    {
        private readonly string _connectionString;
        private readonly LogData _logData;
        public CountryData(string connectionString)
        {
            _connectionString = connectionString;
            _logData = new LogData(connectionString);
        }
        public async Task<List<CountryDTO>> GetAllAsync() {
            try
            {
                List<CountryDTO> countries = new List<CountryDTO>();

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select CountryID,CountryName from Countries;";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    countries.Add(new CountryDTO()
                    {
                        CountryID = reader.GetInt32(reader.GetOrdinal("CountryID")),
                        CountryName = reader.GetString(reader.GetOrdinal("CountryName")),
                    });
                }

                return countries;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "CountryData", "GetAllAsync", ex.Message);
                return new List<CountryDTO>();
            }
        }
    }
}
