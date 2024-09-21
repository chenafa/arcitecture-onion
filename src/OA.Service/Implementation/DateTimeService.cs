using OA.Service.Contract;

namespace OA.Service.Implementation;

public class DateTimeService : IDateTimeService
{
    public DateTime NowUtc => DateTime.UtcNow;
}