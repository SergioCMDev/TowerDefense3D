using Services.Timing;

namespace Services.Interfaces
{
    public interface ITimerGenerator
    {
        Timer GenerateTimer();
    }
}