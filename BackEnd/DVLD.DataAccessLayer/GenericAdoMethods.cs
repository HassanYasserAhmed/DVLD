using Microsoft.Data.SqlClient;
using System;
namespace DVLD.DataAccessLayer.GeneralAdoMethods
    //git add
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
