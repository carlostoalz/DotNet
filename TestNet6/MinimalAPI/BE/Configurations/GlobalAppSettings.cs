using Microsoft.Extensions.Options;

namespace BE
{
    public class GlobalAppSettings
    {
        public AppSetting Settings { get; set; }

        public GlobalAppSettings(IOptions<AppSetting> settings) => Settings = settings.Value;
    }
}
