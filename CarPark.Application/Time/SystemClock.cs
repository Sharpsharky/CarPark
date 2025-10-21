using CarPark.Application.Abstractions.Time;

namespace CarPark.Application.Time
{
    public sealed class SystemClock : IClock
    {
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
