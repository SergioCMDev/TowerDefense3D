using Creeps;
using UnityEngine;
using Utils;

namespace Events
{
    [CreateAssetMenu(fileName = "CreepHasHitBaseEvent",
        menuName = "Events/Creeps/CreepHasHitBaseEvent")]
    public class CreepHasHitBaseEvent : GameEventScriptable
    {
        public Creep creepInstance;
    }
}