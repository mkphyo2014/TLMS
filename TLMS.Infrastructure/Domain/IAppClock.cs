using NodaTime;

namespace TLMS.Infrastructure
{
    
    public interface IAppClock : IClock
    {
        DateTimeZone GetDateTimeZone();

        LocalDateTime GetAppLocalNow();

    }
}