using CommonInterfaces;
using UnityEngine;

namespace Projectiles
{
    public class NormalProjectile : Projectile
    {
        protected override void AttackObject(GameObject objectToAttack)
        {
            objectToAttack.GetComponentInParent<IReceiveDamage>()?.ReceiveDamage(damage);
        }
    }
}