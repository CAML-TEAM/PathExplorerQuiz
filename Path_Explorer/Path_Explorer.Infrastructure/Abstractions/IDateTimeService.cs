namespace Path_Explorer.Infrastructure.Abstractions;

public interface IDateTimeService
{
    DateTime NowUtc { get; }
}
