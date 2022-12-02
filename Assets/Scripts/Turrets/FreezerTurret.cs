using Projectiles;

namespace Turrets
{
    public class FreezerTurret : Turret
    {
        protected internal override void Init()
        {
            Projectile = ProjectileLoaderService.GetProjectileByType(ProjectileType.Freezer);
            base.Init();
        }
    }
}