using CommonInterfaces;
using UnityEngine;

namespace Projectiles
{
    public class FreezerProjectile : Projectile
    {
        [SerializeField] private float durationOfEffect = 2;
        [SerializeField] private float percentageToReduce = 20;
        
        protected override void AttackObject(GameObject objectToAttack)
        {
            objectToAttack.GetComponentInParent<IReceiveDamage>()?.ReceiveDamage(damage);
            objectToAttack.GetComponentInParent<IStatusApplier>()?.ReduceSpeed(percentageToReduce, durationOfEffect);
        }
    }
}