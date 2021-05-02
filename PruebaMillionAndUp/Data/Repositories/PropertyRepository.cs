using BE.Bases;
using Data.Helpers;
using Data.IRepositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class PropertyRepository : GenericRepository, IPropertyRepository
    {
        private readonly string _DbConnection;
        public PropertyRepository(IConfiguration configuration) : base(configuration.GetConnectionString("dbConnection"))
        {
            this._DbConnection = configuration.GetConnectionString("dbConnection");
        }
        public async Task<Property> InsertProperty(Property property)
        {
            using (SqlConnection conn = new(this._DbConnection))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[PROD_Insert_Property]";
                command.Parameters.AddRange(new List<SqlParameter>() 
                {
                    Parameter.NewParameter("@Name", SqlDbType.NVarChar, 600, property.Name),
                    Parameter.NewParameter("@Adress", SqlDbType.NVarChar, 600, property.Adress),
                    Parameter.NewParameter("@Value", SqlDbType.Money, 8, property.Value),
                    Parameter.NewParameter("@Tax", SqlDbType.Money, 8, property.Tax),
                    Parameter.NewParameter("@Year", SqlDbType.Int, 4, property.Year),
                    Parameter.NewParameter("@IdOwner", SqlDbType.Int, 4, property.IdOwner),
                    Parameter.NewParameter("@PropertyImages", SqlDbType.NVarChar, -1, property.PropertyImagesJson()),
                }.ToArray());
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader != null)
                    {
                        property = reader.Translate<Property>().FirstOrDefault();
                        if (property != null)
                        {
                            property.PropertyImages = reader.Translate<PropertyImage>().ToList();
                            property.PropertyTraces = reader.Translate<PropertyTrace>().ToList();
                        }
                    }
                }
                conn.Close();
            }

            return property;
        }

        public async Task<Property> GetProperty(int propertyId)
        {
            Property property = null;
            using (SqlConnection conn = new(this._DbConnection))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[PROD_Get_Property]";
                command.Parameters.Add(Parameter.NewParameter("@IdProperty", SqlDbType.Int, 4, propertyId));
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader != null)
                    {
                        property = reader.Translate<Property>().FirstOrDefault();
                        if (property != null)
                        {
                            property.PropertyImages = reader.Translate<PropertyImage>().ToList();
                            property.PropertyTraces = reader.Translate<PropertyTrace>().ToList();
                        }
                    }
                }
                conn.Close();
            }

            return property;
        }

        public async Task<PropertyTable> GetProperties()
        {
            PropertyTable propertyTable = new();
            using (SqlConnection conn = new(this._DbConnection))
            {
                conn.Open();
                SqlCommand command = conn.CreateCommand();
                command.CommandType = CommandType.StoredProcedure;
                command.CommandText = "[dbo].[PROD_Get_Properties]";
                using (SqlDataReader reader = await command.ExecuteReaderAsync())
                {
                    if (reader != null)
                    {
                        propertyTable.Properties = reader.Translate<Property>().ToList();
                        propertyTable.PropertyImages = reader.Translate<PropertyImage>().ToList();
                        propertyTable.PropertyTraces = reader.Translate<PropertyTrace>().ToList();
                    }
                }
                conn.Close();
            }
            return propertyTable;
        }
    }
}
