using NodaTime;

namespace TLMS.Infrastructure.Domain
{
    
    public interface IAppClock : IClock
    {
        DateTimeZone GetDateTimeZone();

        LocalDateTime GetAppLocalNow();

    }
}