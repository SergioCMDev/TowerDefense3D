using System.Collections.Generic;
using System.Linq;
using Creeps;
using Events;
using Projectiles;
using Services.Interfaces;
using UnityEngine;
using Utils;

namespace Turrets
{
    public abstract class Turret : MonoBehaviour
    {
        [SerializeField] private float distanceToAttack, projectileSpeed, cadence, projectilesToInstantiateAtFirst;
        [SerializeField] protected Transform shotPosition;
        [SerializeField] private OnTurretStopsBeingMovedEvent onTurretStopsBeingMovedEvent;
        [SerializeField] private OnTurretIsBeingMovedEvent onTurretIsBeingMovedEvent;

        protected Projectile Projectile;
        private bool _isActive;
        private readonly List<Creep> _possibleCreepsTargets = new List<Creep>();
        private float _lastTimeAttacked;
        private bool _hasAttackedBefore;
        protected IProjectileLoader ProjectileLoaderService;

        private readonly List<Projectile> _projectilePool = new List<Projectile>();

        private void Awake()
        {
            ProjectileLoaderService = ServiceLocator.Instance.GetService<IProjectileLoader>();
        }

        protected internal virtual void Init()
        {
            for (int i = 0; i < projectilesToInstantiateAtFirst; i++)
            {
                var projectileInstance = Instantiate(Projectile, shotPosition.position, Quaternion.identity);
                projectileInstance.Deactivate();
                _projectilePool.Add(projectileInstance);
            }

            _isActive = true;
        }

        private bool CanAttack()
        {
            _lastTimeAttacked += Time.deltaTime;
            if (!_hasAttackedBefore) return true;
            return _lastTimeAttacked > cadence;
        }

        private void ShotAt(Creep creep)
        {
            _lastTimeAttacked = Time.deltaTime;

            var projectileInstance = GetProjectile();
            if(projectileInstance == null)return;
            projectileInstance.OnProjectileHitsSomething += DisableProjectile;
            projectileInstance.transform.SetPositionAndRotation(shotPosition.position, Quaternion.identity);

            projectileInstance.Init(projectileSpeed, creep);

            _hasAttackedBefore = true;
        }
        
        private Projectile GetProjectile()
        {
            var projectileToActivate = _projectilePool.FirstOrDefault(projectileToCheck => !projectileToCheck.Active);

            if (projectileToActivate == null)
            {
                projectileToActivate = Instantiate(Projectile);
                projectileToActivate.Deactivate();
                _projectilePool.Add(projectileToActivate);
            }

            return projectileToActivate;
        }

        private void DisableProjectile(Projectile obj)
        {
            obj.OnProjectileHitsSomething -= DisableProjectile;
            obj.Deactivate();
        }

        void Update()
        {
            if (!_isActive) return;
            var creepsToAttack = GetReachableCreeps();
            if (!creepsToAttack.Any()) return;
            foreach (var creep in creepsToAttack)
            {
                if (CanAttack())
                {
                    ShotAt(creep);
                }
            }
        }

        private bool CreepIsNear(Component creep, Component turret)
        {
            return Utilities.ObjectsAreClose(creep.transform.position, turret.transform.position,
                   distanceToAttack);
        }

        private List<Creep> GetReachableCreeps()
        {
            return _possibleCreepsTargets.Where(creep => creep != null && CreepIsNear(creep, this)).ToList();
        }

        public void AddCreepToPossibleTargets(Creep creep)
        {
            if (_possibleCreepsTargets.Contains(creep)) return;
            _possibleCreepsTargets.Add(creep);
        }

        public void RemoveCreepOfPossibleTargets(Creep creep)
        {
            if (!_possibleCreepsTargets.Contains(creep)) return;
            _possibleCreepsTargets.Remove(creep);
        }

        public void IsPicked(bool picked)
        {
            _isActive = !picked;
            if (!picked)
            {
                onTurretStopsBeingMovedEvent.Fire();
            }
            else
            {
                onTurretIsBeingMovedEvent.Fire();
            }
        }

        public bool IsProjectileOfThisTurret(Projectile projectile1)
        {
            return _projectilePool.Contains(projectile1);
        }
    }
}