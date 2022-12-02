using UnityEngine;

namespace Creeps
{
    [CreateAssetMenu(fileName = "CreepConfiguration",
        menuName = "Creeps/CreepConfiguration")]
    public class CreepConfiguration : ScriptableObject
    {
        public float life;
        public int coinsAfterDeath;
        public int damage;
        public float speed;
    }
}