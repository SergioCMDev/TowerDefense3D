using UnityEngine;

namespace Creeps
{
    public class CreepMovement : MonoBehaviour
    {
        private float _initialSpeed;
        private bool _canMove;
        private Vector3 _destination;
        private float _currentSpeed;

        public float InitialSpeed => _initialSpeed;

        public void DoMovement()
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _destination, _currentSpeed * Time.deltaTime);
        }

        void Update()
        {
            if (!_canMove) return;
            DoMovement();
        }

        public void ResetSpeed()
        {
            _currentSpeed = _initialSpeed;
        }

        public void ChangeSpeed(float newSpeed)
        {
            _currentSpeed = newSpeed;
        }

        private void SetLookAtObjective(Transform baseBuilding)
        {
            transform.LookAt(baseBuilding);
        }

        public void Init(float speed, Transform baseBuilding)
        {
            _destination = baseBuilding.position;
            SetLookAtObjective(baseBuilding);
            _initialSpeed = speed;
            _currentSpeed = speed;
            _canMove = true;
        }
    }
}