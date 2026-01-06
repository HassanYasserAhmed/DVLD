using DVLD.DataAccessLayer.DTOs;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.DataAccessLayer
{
    public class LogData
    {
        private readonly string _connectionString;
        public LogData(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<int> AddNewLogAsync(string LayerName, string ClassName,string FunctionName,string Message)
        {
            int NewID = -2;
            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"insert into Logs(LayerName,ClassName,FunctionName,Message)
                               values(@LayerName,@ClassName,@FunctionName,@Message);

                            select SCOPE_IDENTITY();";

            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@LayerName", SqlDbType.NVarChar, 200).Value = LayerName;
            cmd.Parameters.Add("@ClassName", SqlDbType.NVarChar,200).Value = ClassName;
            cmd.Parameters.Add("@FunctionName", SqlDbType.NVarChar,200).Value = FunctionName;
            cmd.Parameters.Add("@Message", SqlDbType.NVarChar,-1).Value = Message;
            await conn.OpenAsync();
            object Result = await cmd.ExecuteScalarAsync();

            if (Result != null && Result != DBNull.Value)
                NewID = Convert.ToInt32(Result);

            return NewID;
        }

    }
}
