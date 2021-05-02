using Data.Helpers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public abstract class GenericRepository
    {
        private string DBConnectionString { get; set; }
        protected GenericRepository(string connectionString) => this.DBConnectionString = connectionString;

        static SqlConnection Connection(string dbConnectionString) => new(dbConnectionString);

        protected async Task<XOuput> GetAsyncFirst<XOuput>(string Name, List<SqlParameter> Parameters, CommandType commandType) where XOuput : new()
        {
            XOuput response = new();
            string dbConnectionString = this.DBConnectionString;
            using (SqlConnection conn = Connection(dbConnectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = commandType;
                command.CommandText = Name;
                if (Parameters != null && Parameters.Count > 0) command.Parameters.AddRange(Parameters.ToArray());
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader != null) response = reader.Translate<XOuput>().FirstOrDefault();
                }
                conn.Close();
            }
            return response;
        }

        protected async Task<int> GetAsyncId(string Name, List<SqlParameter> Parameters, CommandType commandType)
        {
            int response = 0;
            string dbConnectionString = this.DBConnectionString;
            using (SqlConnection conn = Connection(dbConnectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = commandType;
                command.CommandText = Name;
                if (Parameters != null && Parameters.Count > 0) command.Parameters.AddRange(Parameters.ToArray());
                object id = await command.ExecuteScalarAsync();
                response = int.Parse(id.ToString());
                conn.Close();
            }
            return response;
        }

        protected async Task GetAsync(string Name, List<SqlParameter> Parameters, CommandType commandType)
        {
            string dbConnectionString = this.DBConnectionString;
            using SqlConnection conn = Connection(dbConnectionString);
            conn.Open();
            SqlCommand command = conn.CreateCommand();
            command.CommandType = commandType;
            command.CommandText = Name;
            if (Parameters != null && Parameters.Count > 0) command.Parameters.AddRange(Parameters.ToArray());
            await command.ExecuteScalarAsync();
            conn.Close();
        }

        protected async Task<IEnumerable<XOuput>> GetAsync<XOuput>(string Name, List<SqlParameter> Parameters, CommandType commandType) where XOuput : new()
        {
            IEnumerable<XOuput> response = null;
            string dbConnectionString = this.DBConnectionString;
            using (SqlConnection conn = Connection(dbConnectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = commandType;
                command.CommandText = Name;
                if (Parameters != null && Parameters.Count > 0) command.Parameters.AddRange(Parameters.ToArray());
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader != null) response = reader.Translate<XOuput>().ToList();
                }
                conn.Close();
            }
            return response;
        }

        protected async Task<XOuput> GetAsyncMasterDetail<XOuput, XOuputDetail>(string Name, List<SqlParameter> Parameters, CommandType commandType, string PropertyDetailName)
            where XOuput : new()
            where XOuputDetail : new()
        {
            XOuput response = default;
            string dbConnectionString = this.DBConnectionString;
            using (SqlConnection conn = Connection(dbConnectionString))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = commandType;
                command.CommandText = Name;
                if (Parameters != null && Parameters.Count > 0) command.Parameters.AddRange(Parameters.ToArray());
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader != null)
                    {
                        response = reader.Translate<XOuput>().FirstOrDefault();
                        if (response != null)
                        {
                            IEnumerable<XOuputDetail> detail = reader.Translate<XOuputDetail>().ToList();
                            response.GetType().GetProperty(PropertyDetailName).SetValue(response, detail);
                        }
                    }
                }
                conn.Close();
            }
            return response;
        }
    }
}
