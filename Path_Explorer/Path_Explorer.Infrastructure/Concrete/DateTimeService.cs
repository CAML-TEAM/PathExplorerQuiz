using Path_Explorer.Infrastructure.Abstractions;

namespace Path_Explorer.Infrastructure.Concrete;

public class DateTimeService : IDateTimeService
{
    public DateTime NowUtc => DateTime.UtcNow;
}
