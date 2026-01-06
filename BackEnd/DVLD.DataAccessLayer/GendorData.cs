using DVLD.DataAccessLayer.DTOs.General;
using Microsoft.Data.SqlClient;
using System.Data;
namespace DVLD.DataAccessLayer
{
    public class GendorData
    {
        private readonly string _connectionString;
        private readonly LogData _logData;
        public GendorData(string connectionString)
        {
            _connectionString = connectionString;
            _logData = new LogData(connectionString);
        }
        public async Task<List<GendorDTO>> GetAllAsync()
        {
            try
            {
                List<GendorDTO> Gendors = new List<GendorDTO>();

                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select GendorID,GendorName from Gendors;";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                await conn.OpenAsync();

                using SqlDataReader reader = await cmd.ExecuteReaderAsync();

                while (await reader.ReadAsync())
                {
                    Gendors.Add(new GendorDTO()
                    {
                        GendorID = reader.GetInt32(reader.GetOrdinal("GendorID")),
                        GendorName = reader.GetString(reader.GetOrdinal("GendorName")),
                    });
                }
                return Gendors;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "GendorData", "GetAllAsync", ex.Message);
                return new List<GendorDTO>();
            }
        }
    }
}
