using System;
using Creeps;
using Services.Interfaces;
using Services.Timing;
using UnityEngine;
using Utils;

namespace Projectiles
{
    public abstract class Projectile : MonoBehaviour
    {
        [SerializeField] protected float damage;
        [SerializeField] private float distanceToAttack;

        private Vector3 _destination;
        private float _currentSpeed;
        private bool _active;
        private Timer _timer;

        public bool Active => _active;

        public Action<Projectile> OnProjectileHitsSomething;
        private Creep _target;

        public void Init(float speed, Creep creepToAttack)
        {
            _destination = creepToAttack.transform.position - transform.position;
            _target = creepToAttack;
            var timerGeneratorService = ServiceLocator.Instance.GetService<ITimerGenerator>();

            _currentSpeed = speed;
            _active = true;
            gameObject.SetActive(true);

            _timer ??= timerGeneratorService.GenerateTimer();
            _timer.Init(Constansts.TIME_TO_DESTROY_PROJECTIL);
            StartCoroutine(_timer.CountTime());
        }

        void Update()
        {
            if (!Active) return;
            // Debug.Log(
            //     $"ObjectsAreClose{Utilities.ObjectsAreClose(transform.position, _destination, distanceToAttack)}");
            if (_target != null && Utilities.ObjectsAreClose(transform.position, _destination, distanceToAttack))
            {
                AttackObject(_target.gameObject);
                OnProjectileHitsSomething?.Invoke(this);
                return;
            }

            DoMovement();
        }

        protected abstract void AttackObject(GameObject objectToAttack);

        private void DoMovement()
        {
            transform.position += _destination * (_currentSpeed * Time.deltaTime);
        }

        public void Deactivate()
        {
            _active = false;
            gameObject.SetActive(false);
        }
    }
}