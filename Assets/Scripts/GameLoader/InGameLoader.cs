using System.Collections.Generic;
using Events;
using GameTimer;
using Services.Interfaces;
using Spawners;
using SpawnersInteractions;
using UnityEngine;
using Utils;

namespace GameLoader
{
    public class InGameLoader : MonoBehaviour
    {
        [SerializeField] private Transform positionToSpawn;
        [SerializeField] private WavesData wavesData;
        [SerializeField] private Transform baseBuilding;
        [SerializeField] private PlayerHasWonGameEvent playerHasWonGameEvent;
        [SerializeField] private List<CreepSpawnPoint> positionsToSpawn;

        private TurretSpawnerManager _turretSpawnerManager;
        private CreepSpawnerManager _creepSpawnerManager;
        private SpawnersInteractionsController _spawnersInteractionsController;
        private IEconomy _economyManagerService;
        private GameTimerController _gameTimerController;

        public void SpawnTurret(SpawnTurretEvent spawnTurretEvent)
        {
            _turretSpawnerManager.SpawnTurret(spawnTurretEvent.turretCost, spawnTurretEvent.turretToSpawn);
        }

        public void CreepHasHitBase(CreepHasHitBaseEvent creepHasHitBaseEvent)
        {
            _creepSpawnerManager.CreepHasHitBase(creepHasHitBaseEvent.creepInstance);
        }

        public void CreepHasBeenKilled(CreepHasBeenKilledEvent creepHasBeenKilledEvent)
        {
            _creepSpawnerManager.CreepHasBeenKilled(creepHasBeenKilledEvent);

            _economyManagerService.IncreaseCoins(creepHasBeenKilledEvent.CreepInterface.GetCoinsForKilling());
            Destroy(creepHasBeenKilledEvent.creepInstance);
        }

        public void PlayerHasWonGame(PlayerHasWonGameEvent playerHasWonGameEvent)
        {
            _gameTimerController.PlayerHasWonGame();
        }

        public void BaseHasBeenDestroyed(BaseHasBeenDestroyedEvent baseHasBeenDestroyedEvent)
        {
            _gameTimerController.BaseHasBeenDestroyed();
        }

        // Start is called before the first frame update
        void Awake()
        {
            _turretSpawnerManager = new TurretSpawnerManager();
            _spawnersInteractionsController = new SpawnersInteractionsController();
            _creepSpawnerManager = new CreepSpawnerManager();
            _economyManagerService = ServiceLocator.Instance.GetService<IEconomy>();
            _gameTimerController = new GameTimerController();
            ServiceLocator.Instance.RegisterService(gameObject.AddComponent<ObjectGenerator>());
            ServiceLocator.Instance.RegisterService(gameObject.AddComponent<CoroutineExecutioner>());
            _turretSpawnerManager.Init(positionToSpawn);
            _gameTimerController.Init();
            _creepSpawnerManager.Init(wavesData, baseBuilding, positionsToSpawn);
            _creepSpawnerManager.OnPlayerHasWonGame += ThrowPlayerHasWonEvent;
            _spawnersInteractionsController.Init(_turretSpawnerManager, _creepSpawnerManager);
        }

        private void ThrowPlayerHasWonEvent()
        {
            playerHasWonGameEvent.Fire();
        }
    }
}