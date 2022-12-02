using CommonInterfaces;
using Creeps.Interfaces;
using Events;
using Services.Interfaces;
using Services.Timing;
using UI.Views;
using UnityEngine;
using Utils;

namespace Creeps
{
    public enum CreepType
    {
        Normal,
        Big
    }

    public class Creep : MonoBehaviour, IReceiveDamage, IGiveCoins, IStatusApplier
    {
        [SerializeField] private SliderBarView sliderBarView;
        [SerializeField] private CreepConfiguration creepConfiguration;
        [SerializeField] private CreepMovement creepMovement;
        [SerializeField] private BodyCollider bodyCollider;

        [SerializeField] private CreepHasBeenKilledEvent creepHasBeenKilledEvent;
        [SerializeField] private CreepHasHitBaseEvent creepHasHitBaseEvent;

        private float _life;
        private Timer _timer;
        private Coroutine _coroutine;

        private void Awake()
        {
            bodyCollider.OnBodyCollide += CheckCollision;
        }

        private void OnDestroy()
        {
            bodyCollider.OnBodyCollide -= CheckCollision;
            ResetTimerCoroutine();
        }

        public void Init(int index, Transform baseBuilding)
        {
            _timer = ServiceLocator.Instance.GetService<ITimerGenerator>().GenerateTimer();

            Debug.Log($"Initializate {gameObject.name + index} ");
            _life = creepConfiguration.life;
            creepMovement.Init(creepConfiguration.speed, baseBuilding);
            sliderBarView.SetMaxValue(_life);
        }

        private void CheckCollision(Collision collision)
        {
            if (!collision.gameObject.CompareTag("Base")) return;
            var receiveDamage = collision.gameObject.GetComponent<IReceiveDamage>();
            receiveDamage.ReceiveDamage(creepConfiguration.damage);
            creepHasHitBaseEvent.creepInstance = this;
            creepHasHitBaseEvent.Fire();
            Destroy(gameObject);
        }

        public void ReceiveDamage(float receivedDamage)
        {
            _life -= receivedDamage;
            UpdateLifeBar();

            if (!(_life <= 0)) return;
            creepHasBeenKilledEvent.creep = this;
            creepHasBeenKilledEvent.CreepInterface = this;
            creepHasBeenKilledEvent.Fire();
            Destroy(gameObject);
        }

        private void UpdateLifeBar()
        {
            sliderBarView.SetValue(_life);
        }

        public int GetCoinsForKilling()
        {
            return creepConfiguration.coinsAfterDeath;
        }

        public void ReduceSpeed(float percentageToReduce, float effectDuration)
        {
            _timer.Init(effectDuration);
            _timer.OnTimerEnds += ResetSpeedAndTimer;
            if (_coroutine != null)
            {
                ResetTimerCoroutine();
            }
            _coroutine = StartCoroutine(_timer.CountTime());

            creepMovement.ChangeSpeed(creepMovement.InitialSpeed * (1 - percentageToReduce));
        }

        private void ResetSpeedAndTimer()
        {
            _timer.OnTimerEnds -= ResetSpeedAndTimer;
            ResetTimerCoroutine();
            creepMovement.ResetSpeed();
        }

        private void ResetTimerCoroutine()
        {
            StopCoroutine(_coroutine);
            _coroutine = null;
        }
    }
}