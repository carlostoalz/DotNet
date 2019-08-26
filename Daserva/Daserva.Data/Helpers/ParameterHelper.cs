namespace Daserva.Data.Helpers
{
    using System;
    using System.Data;
    using System.Data.SqlClient;

    public class ParameterHelper
    {

        public static SqlParameter NewParameter(string ParameterName, SqlDbType dbType, int Size, object value)
        {
            SqlParameter param = new SqlParameter
            {
                SqlDbType = dbType,
                ParameterName = ParameterName
            };
            if (Size > 0)
            {
                param.Size = Size;
            }
            if (value == null)
            {
                param.Value = DBNull.Value;
            }
            else
            {
                param.Value = value;
            }

            return param;
        }

    }
}
