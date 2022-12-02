using Events;
using Services.Interfaces;
using Services.Utils;
using UnityEngine;

namespace Services
{
    [CreateAssetMenu(fileName = "EconomyManagerService", menuName = "Services/EconomyManagerService")]
    public class EconomyManagerService : LoadableComponent, IEconomy
    {
        public UpdateCoinsEvent updateCoinsEvent;
        public int initialCoins = 50;
        
        private int _coins;
        
        public int Coins => _coins;

        public void IncreaseCoins(int coins)
        {
            _coins += coins;
            updateCoinsEvent.Fire();
        }

        public void ReduceCoins(int coins)
        {
            _coins -= coins;
            updateCoinsEvent.Fire();
        }

        public override void Execute()
        {
            Debug.Log("[EconomyManagerService] Init");
            _coins = initialCoins;
        }
    }
}