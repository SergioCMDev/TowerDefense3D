using Turrets;
using UnityEngine;
using Utils;

namespace Events
{
    [CreateAssetMenu(fileName = "SpawnTurretEvent", menuName = "Events/Turret/SpawnTurretEvent")]
    public class SpawnTurretEvent : GameEventScriptable
    {
        public Turret turretToSpawn;
        public int turretCost;
    }
}