using System.Collections.Generic;
using System.Linq;
using Creeps;
using Services.Interfaces;
using Services.Utils;
using UnityEngine;

namespace Services
{
    [CreateAssetMenu(fileName = "CreepLoaderService", menuName = "Services/CreepLoaderService")]
    public class CreepLoaderService : LoadableComponent, ICreeperLoader
    {
        public List<CreepLoadableData> creepLoadableComponent;

        public override void Execute()
        {
            Debug.Log("[CreepLoaderService] Init");
        }

        public Creep GetPrefabByType(CreepType creepType)
        {
            var possibleData = creepLoadableComponent.SingleOrDefault(x => x.creepType == creepType);
            return possibleData.prefab;
        }
    }
}