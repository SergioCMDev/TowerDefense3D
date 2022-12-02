using System;

namespace Projectiles
{
    [Serializable]
    public struct ProjectileLoadableData
    {
        public ProjectileType projectileType;
        public Projectile prefab;
    }
    public enum ProjectileType
    {
        Normal, Freezer
    }
}