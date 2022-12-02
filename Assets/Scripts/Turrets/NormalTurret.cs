using Projectiles;

namespace Turrets
{
    public class NormalTurret : Turret
    {
        protected internal override void Init()
        {
            Projectile = ProjectileLoaderService.GetProjectileByType(ProjectileType.Normal);
            base.Init();
        }
    }
}