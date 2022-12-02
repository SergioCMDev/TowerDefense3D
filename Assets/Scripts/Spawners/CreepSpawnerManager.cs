using System;
using System.Collections;
using System.Collections.Generic;
using Creeps;
using Events;
using Services.Interfaces;
using UnityEngine;
using Utils;
using Object = UnityEngine.Object;

namespace Spawners
{
    [Serializable]
    public struct EnemyDataInWave
    {
        public CreepType creepType;
        public int numberOfCreeps;
    }

    [Serializable]
    public struct Wave
    {
        public List<EnemyDataInWave> creepInWaves;
        public int timeBetweenCreeps;
    }

    public class CreepSpawnerManager
    {
        private List<CreepSpawnPoint> _positionsToSpawn;
        public Action OnPlayerHasWonGame;
        private WavesData _wavesData;
        private Transform _baseBuilding;
        private float _timeBeforeFirstWaveStarts;
        private readonly float _yValueToUpCreepers = 1;
        private readonly List<Creep> _creepsSpawned = new List<Creep>();
        private int _currentWave;
        private ICreeperLoader _creepLoaderService;
        private IEconomy _economyManagerService;
        private CoroutineExecutioner _coroutineExecutioner;
        private List<Wave> _waves;
        public Action<Creep> OnCreepSpawned;
        public Action<Creep> OnCreepRemoved;

        public List<Creep> CreepsSpawned => _creepsSpawned;


        public void Init(WavesData wavesData, Transform baseBuilding, List<CreepSpawnPoint> creepSpawnPoints)
        {
            _wavesData = wavesData;
            _timeBeforeFirstWaveStarts = _wavesData.timeBeforeFirstWaveStarts;
            _waves = wavesData.waves;
            _baseBuilding = baseBuilding;
            _positionsToSpawn = creepSpawnPoints;
            _currentWave = 0;
            _creepLoaderService = ServiceLocator.Instance.GetService<ICreeperLoader>();
            _economyManagerService = ServiceLocator.Instance.GetService<IEconomy>();
            _coroutineExecutioner = ServiceLocator.Instance.GetService<CoroutineExecutioner>();

            _coroutineExecutioner.StartChildCoroutine(SpawnCreepOfWave(_waves[_currentWave]));
        }

        private IEnumerator SpawnCreepOfWave(Wave wave)
        {
            if (_currentWave == 0)
            {
                yield return new WaitForSeconds(_timeBeforeFirstWaveStarts);
            }

            foreach (var creeps in wave.creepInWaves)
            {
                for (int i = 0; i < creeps.numberOfCreeps; i++)
                {
                    var randomPosition = Utilities.GetRandomValue(0, _positionsToSpawn.Count);
                    var creep = InstantiateCreep(creeps.creepType, _positionsToSpawn[randomPosition]);
                    creep.transform.position += Vector3.up * _yValueToUpCreepers;
                    creep.Init(i, _baseBuilding);
                    CreepsSpawned.Add(creep);
                    OnCreepSpawned?.Invoke(creep);
                    yield return new WaitForSeconds(wave.timeBetweenCreeps);
                }
            }
        }

        public void CreepHasHitBase(Creep creepHasHitBaseEvent)
        {
            UpdateQuantityOfCreeps(creepHasHitBaseEvent);
        }

        public void CreepHasBeenKilled(CreepHasBeenKilledEvent creepHasBeenKilledEvent)
        {
            UpdateQuantityOfCreeps(creepHasBeenKilledEvent.creep);

            _economyManagerService.IncreaseCoins(creepHasBeenKilledEvent.CreepInterface.GetCoinsForKilling());
            Object.Destroy(creepHasBeenKilledEvent.creepInstance);
        }

        private void UpdateQuantityOfCreeps(Creep creep)
        {
            CreepsSpawned.Remove(creep);

            if (CreepsSpawned.Count == 0)
            {
                UpdateWave();
            }

            OnCreepRemoved?.Invoke(creep);
        }

        private void UpdateWave()
        {
            if (_currentWave + 1 >= _waves.Count)
            {
                OnPlayerHasWonGame?.Invoke();
                return;
            }

            _currentWave++;
            _coroutineExecutioner.StartCoroutine(SpawnCreepOfWave(_waves[_currentWave]));
        }

        private Creep InstantiateCreep(CreepType creepType, CreepSpawnPoint spawnPoint)
        {
            var prefab = _creepLoaderService.GetPrefabByType(creepType);
            return spawnPoint.InstantiateCreep(prefab);
        }
    }
}