using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Text;

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
