using Creeps;
using Creeps.Interfaces;
using UnityEngine;
using Utils;

namespace Events
{
    [CreateAssetMenu(fileName = "CreepHasBeenKilledEvent",
        menuName = "Events/Creeps/CreepHasBeenKilledEvent")]
    public class CreepHasBeenKilledEvent : GameEventScriptable
    {
        public IGiveCoins CreepInterface;
        public Creep creep;
        public GameObject creepInstance;
    }
}