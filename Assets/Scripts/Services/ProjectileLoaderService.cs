using System.Collections.Generic;
using System.Linq;
using Projectiles;
using Services.Interfaces;
using Services.Utils;
using UnityEngine;

namespace Services
{
    [CreateAssetMenu(fileName = "ProjectileLoaderService", menuName = "Services/ProjectileLoaderService")]
    public class ProjectileLoaderService : LoadableComponent, IProjectileLoader
    {
        public List<ProjectileLoadableData> projectileLoadableComponent;
        public override void Execute()
        {
            Debug.Log("[ProjectileLoaderService] Init");
        }

        public Projectile GetProjectileByType(ProjectileType projectileType)
        {
            var possibleData = projectileLoadableComponent.SingleOrDefault(x=>x.projectileType == projectileType);
            return possibleData.prefab;
        }
    }
}