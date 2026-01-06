using Microsoft.Data.SqlClient;
using System.Data;
using DVLD.DataAccessLayer.GeneralAdoMethods;
using DVLD.DataAccessLayer.DTOs.Person;
using DVLD.DataAccessLayer.DTOs;
using DVLD.DataAccessLayer.Extensions;

namespace DVLD.DataAccessLayer {
    public class PersonData {
        private readonly string _connectionString;
        private readonly LogData _logData;

        public PersonData(string connectionString) {
            _connectionString = connectionString;
            _logData = new LogData(connectionString);

        }
        public async Task<int> AddAsync(AddNewPersonDTO DTO)
        {
            try { 
            int NewPersonID = -1;
            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"insert into People(FirstName,SecoundName,ThirdName,LastName,
					                            NationalNumber,DateOfBirth,
                                                Phone,Email,Address,
					                            ImageName,GendorID,CountryID)
                            values
                                (@FirstName,@SecoundName,@ThirdName,@LastName,
                                @NationalNumber,@DateOfBirth,@Phone,@Email,@Address,
                                @ImageName,@GendorID,@CountryID);

                            SELECT SCOPE_IDENTITY();";

            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = DTO.FirstName;
            cmd.Parameters.Add("@SecoundName", SqlDbType.NVarChar, 50).Value = DTO.SecoundName;
            cmd.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 50).Value = DTO.ThirdName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = DTO.LastName;
            cmd.Parameters.Add("@NationalNumber", SqlDbType.NVarChar, 100).Value = DTO.NationalNumber;
            cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = DTO.DateOfBirth;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 100).Value = DTO.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = DTO.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = DTO.Address;
            cmd.Parameters.Add("@ImageName", SqlDbType.NVarChar, 100).Value = DTO.ImageName;
            cmd.Parameters.Add("@GendorID", SqlDbType.Int).Value = DTO.GendorID;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = DTO.CountryID;
            await conn.OpenAsync();
            object Result = await cmd.ExecuteScalarAsync();
            if (Result != null && Result != DBNull.Value)
                NewPersonID = Convert.ToInt32(Result);

            return NewPersonID;
        } catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "PersonData", "AddAsync", ex.Message);
                return -1;
            }
}
        public async Task<bool> UpdateAsync(UpdatePersonDTO DTO) {
            using SqlConnection conn = new SqlConnection(_connectionString);

            string Query = @"update People
                                set FirstName=@FirstName,
	                                SecoundName =@SecoundName,
	                                ThirdName = @ThirdName,
	                                LastName = @LastName,
	                                NationalNumber=@NationalNumber,
	                                DateOfBirth=@DateOfBirth,
	                                Phone=@Phone,
	                                Email =@Email,
	                                Address=@Address,
	                                ImageName=@ImageName,
	                                GendorID = @GendorID,
	                                CountryID = @CountryID
                                where PersonID=@PersonID and IsDeleted=0;";
            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = DTO.PersonID;
            cmd.Parameters.Add("@FirstName", SqlDbType.NVarChar, 50).Value = DTO.FirstName;
            cmd.Parameters.Add("@SecoundName", SqlDbType.NVarChar, 50).Value = DTO.SecoundName;
            cmd.Parameters.Add("@ThirdName", SqlDbType.NVarChar, 50).Value = DTO.ThirdName;
            cmd.Parameters.Add("@LastName", SqlDbType.NVarChar, 50).Value = DTO.LastName;
            cmd.Parameters.Add("@NationalNumber", SqlDbType.NVarChar, 100).Value = DTO.NationalNumber;
            cmd.Parameters.Add("@DateOfBirth", SqlDbType.Date).Value = DTO.DateOfBirth;
            cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 100).Value = DTO.Phone;
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 100).Value = DTO.Email;
            cmd.Parameters.Add("@Address", SqlDbType.NVarChar, 100).Value = DTO.Address;
            cmd.Parameters.Add("@ImageName", SqlDbType.NVarChar, 100).Value = DTO.ImageName;
            cmd.Parameters.Add("@GendorID", SqlDbType.Int).Value = DTO.GendorID;
            cmd.Parameters.Add("@CountryID", SqlDbType.Int).Value = DTO.CountryID;
            await conn.OpenAsync();

            int RowsAffected = await cmd.ExecuteNonQueryAsync();

            return RowsAffected > 0;
        }
        public async Task<bool> DeleteAsync(int PersonID) {
            try {
            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"update People
                                set isdeleted =1
                                where PersonID=@PersonID
	                                  and IsDeleted=0";
            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;
            await conn.OpenAsync();
            int RowsAffected = await cmd.ExecuteNonQueryAsync();

            return RowsAffected > 0;
        } catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "PersonData", "UpdateAsync", ex.Message);
                return false;
            }
}
        public async Task<bool> ChangeImageNameAsync(ChangeImageNameDTO DTO) {
            try {
            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"update People
                                set ImageName=@ImageName
                                where PersonID=@PersonID 
                                       and IsDeleted=0";
            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = DTO.PersonID;
            cmd.Parameters.Add("@ImageName", SqlDbType.NVarChar, 500).Value = DTO.ImageName;

            await conn.OpenAsync();

            int RowsAffected = await cmd.ExecuteNonQueryAsync();

            return RowsAffected > 0;
        } catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "PersonData", "ChangeImageNameAsync", ex.Message);
                return false;
            }
}
        public async Task<List<GetPersonDTO>> GetAllAsync() {
            try {
            List<GetPersonDTO> users = new List<GetPersonDTO>();

            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"select PersonID,FirstName,SecoundName,ThirdName,
                                LastName,NationalNumber,DateOfBirth,Phone,Email,
                                Address,ImageName,Gendors.GendorName,
                                Countries.CountryName
                                from People
                                inner join Gendors 
	                                on Gendors.GendorID=People.GendorID
                                inner join Countries
	                                on Countries.CountryID=People.CountryID
                                where  People.IsDeleted=0";

            using SqlCommand cmd = new SqlCommand(Query, conn);
            await conn.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();

            while (await reader.ReadAsync())
            {
                users.Add(new GetPersonDTO()
                {
                    PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                    FirstName = reader.GetNullableString("FirstName"),
                    SecoundName = reader.GetNullableString("SecoundName"),
                    ThirdName = reader.GetNullableString("ThirdName"),
                    LastName = reader.GetNullableString("LastName"),
                    NationalNumber = reader.GetString(reader.GetOrdinal("NationalNumber")),
                    DateOfBirth = reader.GetDateOnly("DateOfBirth"),
                    Phone = reader.GetNullableString("Phone"),
                    Email = reader.GetNullableString("Email"),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    ImageName = reader.GetNullableString("ImageName"),
                    GendorName = reader.GetString(reader.GetOrdinal("GendorName")),
                    CountryName = reader.GetString(reader.GetOrdinal("CountryName"))
                });
            }

            return users;
        } catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "PersonData", "GetAllAsync", ex.Message);
                return null;
            }
}
        public async Task<bool> IsEmailExistsAsync(string Email) {
            try {
            using SqlConnection conn = new SqlConnection(_connectionString);
            string query = @"SELECT TOP (1) 1 FROM People
                                WHERE Email = @Email and isdeleted=0";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@Email", SqlDbType.NVarChar, 50).Value = Email;

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();

            return result != null;
        } catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "PersonData", "IsEmailExistsAsync", ex.Message);
                return false;
            }
}
        public async Task<bool> IsPhoneExistsAsync(string Phone) {
            try
            {
                using SqlConnection conn = new SqlConnection(_connectionString);
                string query = @"SELECT TOP (1) 1 FROM People
                                     WHERE Phone = @Phone and IsDeleted=0";

                using SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add("@Phone", SqlDbType.NVarChar, 50).Value = Phone;

                await conn.OpenAsync();
                var result = await cmd.ExecuteScalarAsync();

                return result != null;
            } catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "PersonData", "IsPhoneExistsAsync", ex.Message);
                return false;
            }
        }
        public async Task<bool> IsNationalNumberExistsAsync(string NationalNumber) {
            try { 
            using SqlConnection conn = new SqlConnection(_connectionString);
            string query = @"SELECT TOP (1) 1 FROM People
                               WHERE NationalNumber = @NationalNumber
                               and IsDeleted=0";

            using SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.Add("@NationalNumber", SqlDbType.NVarChar, 100).Value = NationalNumber;

            await conn.OpenAsync();
            var result = await cmd.ExecuteScalarAsync();

            return result != null;
        } catch (Exception ex)
     {
         await _logData.AddNewLogAsync("DAL", "PersonData", "IsNationalNumberExistsAsync", ex.Message);
         return false;
     }
}
        public async Task<bool> HasDataAsync(int PerosnID) {
            try { 
            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"select top (1) 1 from users where PersonID=@PersonID and IsDeleted=0";
            using SqlCommand cmd = new SqlCommand(Query, conn);
            cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = PerosnID;
            await conn.OpenAsync();

            object Result = await cmd.ExecuteScalarAsync();

            return Result != null;
        } catch (Exception ex)
            {
                await _logData.AddNewLogAsync("DAL", "PersonData", "HasDataAsync", ex.Message);
                return false;
            }

        }
        public async Task<GetPersonDTO> GetByIDAsync(int PersonID)
        {
            try {
            GetPersonDTO user = null;

            using SqlConnection conn = new SqlConnection(_connectionString);
            string Query = @"select PersonID,FirstName,SecoundName,ThirdName,
                                LastName,NationalNumber,DateOfBirth,Phone,Email,
                                Address,ImageName,Gendors.GendorName,
                                Countries.CountryName
                                from People
                                inner join Gendors 
	                                on Gendors.GendorID=People.GendorID
                                inner join Countries
	                                on Countries.CountryID=People.CountryID
                                where People.PersonID=@PersonID
                                 and People.IsDeleted=0";

            using SqlCommand cmd = new SqlCommand(Query, conn);
                cmd.Parameters.Add("@PersonID", SqlDbType.Int).Value = PersonID;

            await conn.OpenAsync();

            using SqlDataReader reader = await cmd.ExecuteReaderAsync();


            if (await reader.ReadAsync())
            {
                user = new GetPersonDTO()
                {
                    PersonID = reader.GetInt32(reader.GetOrdinal("PersonID")),
                    FirstName = reader.GetString(reader.GetOrdinal("FirstName")),
                    SecoundName = reader.GetString(reader.GetOrdinal("SecoundName")),
                    ThirdName = reader.GetString(reader.GetOrdinal("ThirdName")),
                    NationalNumber = reader.GetString(reader.GetOrdinal("NationalNumber")),
                    DateOfBirth = reader.GetDateOnly("DateOfBirth"),
                    Phone = reader.GetString(reader.GetOrdinal("Phone")),
                    Email = reader.GetString(reader.GetOrdinal("Email")),
                    Address = reader.GetString(reader.GetOrdinal("Address")),
                    ImageName = reader.GetString(reader.GetOrdinal("ImageName")),
                    GendorName = reader.GetString(reader.GetOrdinal("GendorName")),
                    CountryName = reader.GetString(reader.GetOrdinal("CountryName"))
                };
            }
            return user;
        } catch (Exception ex)
            {
            await _logData.AddNewLogAsync("DAL", "PersonData", "GetByIDAsync", ex.Message);
            return null;
        }
}
    } 
    }