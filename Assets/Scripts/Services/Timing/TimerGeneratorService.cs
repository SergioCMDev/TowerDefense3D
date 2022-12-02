using Services.Interfaces;
using Services.Utils;
using UnityEngine;

namespace Services.Timing
{
    [CreateAssetMenu(fileName = "TimerGeneratorService", menuName = "Services/TimerGeneratorService")]
    public class TimerGeneratorService : LoadableComponent, ITimerGenerator
    {
        public Timer GenerateTimer()
        {
            var timer = new Timer();
            return timer;
        }

        public override void Execute()
        {
            Debug.Log("[TimerGeneratorService] Iniciamos inicializacion");
        }
    }
}