using Creeps;
using Spawners;
using Turrets;

namespace SpawnersInteractions
{
    public class SpawnersInteractionsController
    {
        private CreepSpawnerManager _creepSpawnerManager;
        private TurretSpawnerManager _turretSpawnerManager;

        public void Init(TurretSpawnerManager spawnerManager, CreepSpawnerManager creepSpawnerManager)
        {
            _creepSpawnerManager = creepSpawnerManager;
            _turretSpawnerManager = spawnerManager;
            _creepSpawnerManager.OnCreepSpawned += AddCreepToTurrets;
            _creepSpawnerManager.OnCreepRemoved += RemoveCreepOfTurrets;

            _turretSpawnerManager.OnTurretSpawned += AddCreepToTurret;
        }

        private void AddCreepToTurret(Turret turret)
        {
            foreach (var creep in _creepSpawnerManager.CreepsSpawned)
            {
                turret.AddCreepToPossibleTargets(creep);
            }
        }

        private void AddCreepToTurrets(Creep creep)
        {
            _turretSpawnerManager.AddCreepToTurret(creep);
        }

        private void RemoveCreepOfTurrets(Creep creep)
        {
            _turretSpawnerManager.RemoveCreepOfTurret(creep);
        }
    }
}