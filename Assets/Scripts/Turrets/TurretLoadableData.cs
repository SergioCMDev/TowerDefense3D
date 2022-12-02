using System;
using UI;

namespace Turrets
{
    [Serializable]
    public struct TurretLoadableData
    {
        public TurretType turretType;
        public Turret prefab;
        public int cost;
    }
}