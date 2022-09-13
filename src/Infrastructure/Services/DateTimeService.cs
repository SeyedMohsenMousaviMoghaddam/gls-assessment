using GLS.Application.Common.Interfaces;

namespace GLS.Infrastructure.Services;

public class DateTimeService : IDateTime
{
    public DateTime Now => DateTime.Now;
}
