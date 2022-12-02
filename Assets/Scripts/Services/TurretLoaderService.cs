using System.Collections.Generic;
using System.Linq;
using Services.Interfaces;
using Services.Utils;
using Turrets;
using UnityEngine;

namespace Services
{
    [CreateAssetMenu(fileName = "TurretLoaderService", menuName = "Services/TurretLoaderServiceLoaderService")]
    public class TurretLoaderService : LoadableComponent, ITurretLoader
    {
        public List<TurretLoadableData> turretLoadableComponent;
        public override void Execute()
        {
            Debug.Log("[TurretLoaderService] Init");
        }

        public TurretLoadableData GetTurretByType(TurretType turretType)
        {
            var possibleData = turretLoadableComponent.SingleOrDefault(x=>x.turretType == turretType);
            return possibleData;
        }
    }
}