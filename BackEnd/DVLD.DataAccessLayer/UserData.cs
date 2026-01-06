using DVLD.DataAccessLayer.DTOs.User;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DVLD.DataAccessLayer {
    public class UserData {
        private readonly string _connectionString;
        private readonly LogData _logData;
        public UserData(string connectionString) {
            _connectionString = connectionString;
            _logData = new LogData(connectionString);
        }
        public async Task<string> GetCurrentPasswordAsync(int UserID)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

                string Query = @"select password from Users
                                 where UserID=@UserID
                                    and IsDeleted=0;";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

                await conn.OpenAsync();

            string result = Convert.ToString(await cmd.ExecuteScalarAsync());

                return result;

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "UserData", "GetCurrentPasswordAsync", ex.Message);
                return null;
            }
        }
        public async Task<int> AddNewAsync(AddNewUserDTO DTO) {
            try
            {
                int NewID = -2;
            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"insert into Users (PersonID,UserName,Password)
                            values(@PersonID,@UserName,@Password);
                            select SCOPE_IDENTITY();";

            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = DTO.PersonID;
            cmd.Parameters.Add("@UserName", SqlDbType.NVarChar, 100).Value = DTO.UserName;
            cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 100).Value = DTO.Password;
            await conn.OpenAsync();
            object Result = await cmd.ExecuteScalarAsync();

            if (Result != null && Result != DBNull.Value)
                    NewID = Convert.ToInt32((decimal)Result);

                return NewID;

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL","UserData", "AddNewAsync", ex.Message);
                return -1;
            }
        }
        public async Task<bool> DeleteAsync(int UserID) {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

            string Query = @"update Users
                                set IsDeleted=1
                            where UserID=@UserID
                                  and IsDeleted=0";

            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

            await conn.OpenAsync();

            int RowsAffected = await cmd.ExecuteNonQueryAsync();

            return RowsAffected > 0;
                }
                catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL","UserData", "DeleteAsync", ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateActivation(ChangeUserActivationDTO DTO) {
            try
            {

                using SqlConnection conn = new SqlConnection(_connectionString);

            string Query = @"update Users
                                set IsActive=@IsActive
                             where UserID=@UserID";

            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = DTO.UserID;
            cmd.Parameters.Add("@IsActive", SqlDbType.Bit).Value = DTO.IsActive;

            await conn.OpenAsync();

            int RowsAffected = await cmd.ExecuteNonQueryAsync();

            return RowsAffected > 0;
            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL","UserData", "UpdateActivation", ex.Message);
                return false;
            }
        }
        public async Task<bool> ChangePasswordAsync(ChangePasswordDTO DTO)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

                string Query = @"update Users
                              set Password=@NewPassword
                            where UserID=@UserID";

                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = DTO.UserID;
                cmd.Parameters.Add("@NewPassword", SqlDbType.NVarChar, 100).Value = DTO.NewPassword;

                await conn.OpenAsync();

                int RowsAffected = await cmd.ExecuteNonQueryAsync();

                return RowsAffected > 0;
            }catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "UserData","ChangePasswordAsync", ex.Message);
                return false;
            }
        }
        public async Task<bool> UpdateTokenAsync(UpdateTokenDTO DTO) {
           try {
            using SqlConnection conn = new SqlConnection(_connectionString);

            string Query = @"update Users
                                set Token=@NewToken,
	                                TokenExpiredAt=@NewExpiryDate
                                where UserID=@UserID";
            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = DTO.UserID;
            cmd.Parameters.Add("@NewToken", SqlDbType.NVarChar, 200).Value = DTO.Token;
            cmd.Parameters.Add("@NewExpiryDate", SqlDbType.DateTime).Value = DTO.ExpiryDate;

            await conn.OpenAsync();

            int RowsAffected = await cmd.ExecuteNonQueryAsync();

            return RowsAffected > 0;
        }catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL","UserData","UpdateTokenAsync", ex.Message);
                return false;
            }
        }
        public async Task<GetUserDTO> GetByIDAsync(int UserID) {
            try
            {
                GetUserDTO user = null;

            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"select UserID,UserName,
		                            P.FullName,U.CreatedAt,U.IsActive
                             from Users as U
                                    inner join People as P
	                                  on P.PersonID=U.PersonID
                            where U.UserID=@UserID
	                              and Users.IsDeleted=0;";

            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@UserID", SqlDbType.Int).Value = UserID;

            await conn.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                user = new GetUserDTO()
                {
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    PersonID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                };
            }
            return user;

            }catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL","UserData", "GetByIDAsync", ex.Message);
                return null;
            }
        }
        public async Task<GetUserDTO> GetByEmailAsync(string Email) {
            try
            {
                GetUserDTO user = null;

            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"select UserID,UserName,
		                            P.FullName,U.CreatedAt,U.IsActive
                             from Users as U
                                    inner join People as P
	                                  on P.PersonID=U.PersonID
                            where P.Email=@Email
	                              and Users.IsDeleted=0;";

            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = Email;

            await conn.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            if (await reader.ReadAsync())
            {
                user = new GetUserDTO()
                {
                    UserID = reader.GetInt32(reader.GetOrdinal("UserID")),
                    UserName = reader.GetString(reader.GetOrdinal("UserName")),
                    FullName = reader.GetString(reader.GetOrdinal("FullName")),
                    CreatedAt = reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                    IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive"))
                };
            }
            return user;

            }
            catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL","UserData", "GetByIDAsync", ex.Message);
                return null;
            }
        }
        public async Task<string> GetRoleByTokenAsync(string Token) {
            try {
                string Role = string.Empty;
                using SqlConnection conn = new SqlConnection(_connectionString);
                string Query = @"select RoleName from Users
                            inner join Roles on roles.RoleID=users.RoleID
                            where users.IsDeleted=0
                                  and Token=@Token
	                              and TokenExpiredAt > GETDATE();";
                using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@Token", SqlDbType.NVarChar, 200).Value = Token;
                await conn.OpenAsync();
                object Result = await cmd.ExecuteScalarAsync();

                if (Result != null && Result != DBNull.Value)
                    Role = Convert.ToString(Result);

                return Role;
            } catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL","UserData", "GetRoleByTokenAsync", ex.Message);
                return null;
            }
        }
        public async Task<string> GetRoleByEmailAsync (string Email) {
            try
            {
                string Role = string.Empty;
            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"select Roles.RoleName from Users
                                inner join People
		                                on Users.PersonID=People.PersonID
                                inner join Roles
		                                on Roles.RoleID=users.RoleID
                                where users.IsDeleted=0
	                                and users.IsActive=1
	                                and users.TokenExpiredAt > GETDATE()
	                                and People.Email =@Email;";

            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 200).Value = Email;
            await conn.OpenAsync();
            object Result = await cmd.ExecuteScalarAsync();

            if (Result != null && Result != DBNull.Value)
                Role = Convert.ToString(Result);

            return Role;

            }catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL","UerData", "GetRoleByEmailAsync", ex.Message);
                return null;
            }
        }
        public async Task<bool> IsUsernameUsed(string UserName) {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);

            string Query = @"select top (1) 1 from users
                                where UserName=@Username";
            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@Username", SqlDbType.NVarChar, 100).Value = UserName;

            await conn.OpenAsync();

            object result = await cmd.ExecuteScalarAsync();

            return result != null;
               
            }catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL","UserData", "IsUsernameUsed", ex.Message);
                return false;
            }
        }
    }
}
