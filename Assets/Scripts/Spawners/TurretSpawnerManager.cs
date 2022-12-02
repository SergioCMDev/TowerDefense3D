using System;
using System.Collections.Generic;
using Creeps;
using Services.Interfaces;
using Turrets;
using UnityEngine;
using Utils;

namespace Spawners
{
    public class TurretSpawnerManager 
    {
        private Transform _positionToSpawn;
        public Action<Turret> OnTurretSpawned;

        private IEconomy _economyManagerService;
        private ObjectGenerator _objectGenerator;
        private readonly List<Turret> _spawnedTurrets = new List<Turret>();
        
        public void Init(Transform positionToSpawn)
        {
            _positionToSpawn = positionToSpawn;
            _economyManagerService = ServiceLocator.Instance.GetService<IEconomy>();
            _objectGenerator = ServiceLocator.Instance.GetService<ObjectGenerator>();
        }
        
        public void SpawnTurret(int turretCost, Turret turretToSpawn)
        {
            _economyManagerService.ReduceCoins(turretCost);

            var turret = (Turret)_objectGenerator.InstantiateObject<Turret>(turretToSpawn, _positionToSpawn.position, Quaternion.identity);
            turret.Init();
            _spawnedTurrets.Add(turret);
            OnTurretSpawned?.Invoke(turret);
        }

        public void RemoveCreepOfTurret(Creep creep)
        {
            foreach (var turret in _spawnedTurrets)
            {
                turret.RemoveCreepOfPossibleTargets(creep);
            }
        }

        public void AddCreepToTurret(Creep creep)
        {
            foreach (var turret in _spawnedTurrets)
            {
                turret.AddCreepToPossibleTargets(creep);
            }
        }
    }
}