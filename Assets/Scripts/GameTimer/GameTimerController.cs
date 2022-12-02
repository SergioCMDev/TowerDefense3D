using Services.Interfaces;
using Utils;

namespace GameTimer
{
    public class GameTimerController
    {
        private IGameTimer _timerManager;

        public void Init()
        {
            _timerManager = ServiceLocator.Instance.GetService<IGameTimer>();
        }

        public void PlayerHasWonGame()
        {
            _timerManager.PauseGame();
        }

        public void BaseHasBeenDestroyed()
        {
            _timerManager.PauseGame();
        }
    }
}