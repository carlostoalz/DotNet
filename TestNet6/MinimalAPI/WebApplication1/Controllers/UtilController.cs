using Domain;

namespace Controllers
{
    public static class UtilController
    {
        public static void UtilRoutes(this WebApplication app)
        {
            app.MapGet("api/Util/GetTimezones", async (IUtilBL bl) => await bl.GetTimezones());
        }
    }
}
