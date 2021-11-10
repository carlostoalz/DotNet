using Domain;
using Presentacion.Extensions;

namespace Controllers
{
    public static class UtilController
    {
        public static void UtilRoutes(this WebApplication app)
        {
            app.MapGet("api/Util/GetTimezones", async (IUtilBL bl) =>  Results.Extensions.ResultResponse(await bl.GetTimezones()));
        }
    }
}
