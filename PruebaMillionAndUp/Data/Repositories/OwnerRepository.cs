using BE.Bases;
using Data.Helpers;
using Data.IRepositories;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class OwnerRepository : GenericRepository , IOwnerRepository
    {
        public OwnerRepository(IConfiguration configuration) : base(configuration.GetConnectionString("dbConnection")) { }

        public async Task<Owner> InsertOwner(Owner owner)
        {
            List<SqlParameter> parameters = new()
            {
                Parameter.NewParameter("@Name", SqlDbType.NVarChar, 600, owner.Name),
                Parameter.NewParameter("@Adress", SqlDbType.NVarChar, 600, owner.Adress),
                Parameter.NewParameter("@Photo", SqlDbType.NVarChar, 600, owner.Photo),
                Parameter.NewParameter("@Birthday", SqlDbType.Date, 3, owner.Birthday),
                Parameter.NewParameter("@Email", SqlDbType.NVarChar, 400, owner.Email),
                Parameter.NewParameter("@Passord", SqlDbType.NVarChar, 400, owner.Passord),
            };

            owner.IdOwner = await this.GetAsyncId("[dbo].[PROD_Insert_Owner]", parameters, CommandType.StoredProcedure);

            return owner;
        }

        public async Task<Owner> GetOwner(string email)
        {
            List<SqlParameter> parameters = new()
            {
                Parameter.NewParameter("@Email", SqlDbType.NVarChar, 400, email)
            };

            return await this.GetAsyncFirst<Owner>("[dbo].[PROD_Get_Owner]", parameters, CommandType.StoredProcedure);
        }
    }
}
