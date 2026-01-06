using Microsoft.Data.SqlClient;

namespace DVLD.DataAccessLayer.Extensions
{
    public static class SqlDataReaderExtensions
    {
        public static string? GetNullableString(this SqlDataReader reader, string column)
        {
            int index = reader.GetOrdinal(column);
            return reader.IsDBNull(index) ? null : reader.GetString(index);
        }

        public static int? GetNullableInt(this SqlDataReader reader, string column)
        {
            int index = reader.GetOrdinal(column);
            return reader.IsDBNull(index) ? null : reader.GetInt32(index);
        }

        public static DateOnly? GetNullableDateOnly(this SqlDataReader reader, string column)
        {
            int index = reader.GetOrdinal(column);
            return reader.IsDBNull(index)
                ? null
                : DateOnly.FromDateTime(reader.GetDateTime(index));
        }

        public static DateTime? GetNullableDateTime(this SqlDataReader reader, string column)
        {
            int index = reader.GetOrdinal(column);
            return reader.IsDBNull(index) ? null : reader.GetDateTime(index);
        }
    }
}
