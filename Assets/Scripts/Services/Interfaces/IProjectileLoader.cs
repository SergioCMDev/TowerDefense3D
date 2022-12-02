using Projectiles;

namespace Services.Interfaces
{
    public interface IProjectileLoader
    {
        Projectile GetProjectileByType(ProjectileType projectileType);
    }
}