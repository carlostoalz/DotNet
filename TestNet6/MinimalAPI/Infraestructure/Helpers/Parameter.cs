using System.Data;
using System.Data.SqlClient;

namespace Infraestructure.Helpers
{
    public static class Parameter
    {
        public static SqlParameter NewParameter(string ParameterName,
                                                    SqlDbType dbType,
                                                    int Size,
                                                    object value,
                                                    ParameterDirection parameterDirection = ParameterDirection.Input)
        {
            SqlParameter param = new()
            {
                SqlDbType = dbType,
                ParameterName = ParameterName,
                Direction = parameterDirection
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
