using Creeps;
using UnityEngine;

namespace Spawners
{
    public class CreepSpawnPoint : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoint;

        public Creep InstantiateCreep( Creep slimeToGeneratePrefab)
        {
            var instance = Instantiate(slimeToGeneratePrefab, spawnPoint.position, Quaternion.identity);
            return instance;
        }
    }
}
