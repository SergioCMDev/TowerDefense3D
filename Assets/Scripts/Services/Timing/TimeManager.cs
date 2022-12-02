using Services.Interfaces;
using Services.Utils;
using UnityEngine;

namespace Services.Timing
{
    [CreateAssetMenu(fileName = "TimeManager", menuName = "Services/TimeManager")]
    public class TimeManager : LoadableComponent, IGameTimer
    {
        public void PauseGame()
        {
            Time.timeScale = 0f;
        }
        
        public override void Execute()
        {
            Debug.Log("[TimeManager] INIT");
        }
    }
}