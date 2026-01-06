using DVLD.DataAccessLayer;
using Microsoft.Data.SqlClient;
using System.Data;
    public class CrudOperationsWithAdo
    {
        private readonly string _connectionString;
    private readonly LogData _logData;
        public CrudOperationsWithAdo(string connectionString)
        {
            _connectionString = connectionString;
        }
    //***************************************************************************************************
    //**************(  This Template I Use To Save My Time By Using These CRUD Operations  )************// 
    //***************************************************************************************************
    //Add And Return New ID
    public async Task<int> AddAsync(string StringValue, int IntValue, DateTime DateTimeValue)
    {
        try { 

        int NewID = -1;
        using SqlConnection conn = new SqlConnection(_connectionString);
        string Query = @"";

        using SqlCommand cmd = new SqlCommand(Query, conn);
        cmd.Parameters.Add("", SqlDbType.NVarChar, 50).Value = StringValue;
        cmd.Parameters.Add("", SqlDbType.Int).Value = IntValue;
        cmd.Parameters.Add("", SqlDbType.DateTime).Value = DateTimeValue;
        await conn.OpenAsync();
        object Result = await cmd.ExecuteScalarAsync();

        if (Result != null && Result != DBNull.Value)
            NewID = Convert.ToInt32((decimal)Result);
        return NewID;
    } catch (Exception ex)
            {
            await _logData.AddNewLogAsync("DAL", "", "", ex.Message);
            return -1;
        }
    }
        //Update Or Delete and Return True Or False Baised On Number Of Affected Rows
        public async Task<bool> UpdateAsync(string StringValue, int IntValue, DateTime DateTimeValue)
        {
        try {
            using SqlConnection conn = new SqlConnection(_connectionString);

            string Query = @"";
            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("", SqlDbType.NVarChar, 50).Value = StringValue;
            cmd.Parameters.Add("", SqlDbType.Int).Value = IntValue;
            cmd.Parameters.Add("", SqlDbType.DateTime).Value = DateTimeValue;

            await conn.OpenAsync();

        // Use It If  You Want To Return first Row Of First Column

        //object result = await cmd.ExecuteScalarAsync();

        //return result != null;


        int RowsAffected = await cmd.ExecuteNonQueryAsync();

            return RowsAffected > 0;

    } catch (Exception ex)
            {
            await _logData.AddNewLogAsync("DAL", "", "", ex.Message);
            return false;
        }
        }
        //Get Signle Object From Database
        public async Task<SigleObject> GetByIDAsync(int UserID)
        {
        try { 
            SigleObject user = null;

            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"";

            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("",SqlDbType.Int).Value = UserID;

            await conn.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                user = new SigleObject()
                {
                    ID = reader.GetInt32(reader.GetOrdinal("")),
                    Name = reader.GetString(reader.GetOrdinal("")),
                    DateOfBirth = reader.GetDateTime(reader.GetOrdinal("")),
                    IsActive  = reader.GetBoolean(reader.GetOrdinal(""))
                };
            }
            return user;

        }
        catch (Exception ex)
        {
            await _logData.AddNewLogAsync("DAL", "", "", ex.Message);
            return null;
        }
    }
    //Get List Of Objects From Database
    public async Task<List<SigleObject>> GetAllAsync()
    {
        try { 
        List<SigleObject> users = new List<SigleObject>();

        using SqlConnection conn = new SqlConnection(_connectionString);
        string Query = @"";

        using SqlCommand cmd = new SqlCommand(Query, conn);
        await conn.OpenAsync();

        using SqlDataReader reader = await cmd.ExecuteReaderAsync();

        while (await reader.ReadAsync())
        {
            users.Add(new SigleObject()
            {
                ID = reader.GetInt32(reader.GetOrdinal("")),
                Name = reader.GetString(reader.GetOrdinal("")),
                DateOfBirth = reader.GetDateTime(reader.GetOrdinal("")),
                IsActive = reader.GetBoolean(reader.GetOrdinal(""))
            });
        }

        return users;
    } catch (Exception ex)
            {
            await _logData.AddNewLogAsync("DAL", "", "", ex.Message);
            return new List<SigleObject>();
        }
    }
}
public class SigleObject
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
    }