using NodaTime;
using ServiceStack.Configuration;
using TLMS.Infrastructure.Domain;

namespace TLMS.Infrastructure.Init
{
    public class AppClock : IAppClock
    {
        private readonly IAppSettings _appSettings;
        private DateTimeZone _appTimeZone;

        public AppClock(IAppSettings appSettings)
        {
            _appSettings = appSettings;
            _appTimeZone = DateTimeZoneProviders.Tzdb.GetZoneOrNull(_appSettings.GetString("app.IANATimeZone"));
        }

        public Instant Now => SystemClock.Instance.Now;
        public DateTimeZone GetDateTimeZone() => _appTimeZone;
        public LocalDateTime GetAppLocalNow() => Now.InZone(_appTimeZone).LocalDateTime;


    }
}