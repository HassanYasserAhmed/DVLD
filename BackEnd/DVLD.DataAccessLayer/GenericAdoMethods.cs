using Microsoft.Data.SqlClient;
namespace DVLD.DataAccessLayer.GeneralAdoMethods
{
    public static  class GenericAdoMethods
    {
        public static DateOnly GetDateOnly(this SqlDataReader reader, string column)
        {
            return DateOnly.FromDateTime(
                reader.GetDateTime(reader.GetOrdinal(column))
            );
        }
    }
}
