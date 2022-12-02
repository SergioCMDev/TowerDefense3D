using Events;
using Turrets;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CameraUtils
{
    public class Dragger : MonoBehaviour
    {
        [SerializeField] private int maxDistanceToCheck;
        [SerializeField] private LayerMask layerMask;
        private Turret _clickedObject;
        private bool _gameHasEnded;
        private Camera _camera;
        private readonly RaycastHit[] _raycastHits = new RaycastHit[1];

        private void Start()
        {
            _camera = Camera.main;
        }

        public void BaseHasBeenDestroyed(BaseHasBeenDestroyedEvent baseHasBeenDestroyedEvent)
        {
            _gameHasEnded = true;
        }

        public void PlayerHasWon(PlayerHasWonGameEvent baseHasBeenDestroyedEvent)
        {
            _gameHasEnded = true;
        }

        private void Update()
        {
            if (_gameHasEnded) return;
            if (!Input.GetMouseButton(0))
            {
                if (!_clickedObject) return;
                _clickedObject.IsPicked(false);
                _clickedObject = null;
                return;
            }

            if (EventSystem.current.IsPointerOverGameObject()) return;

            if (_camera != null)
            {
                var point = _camera.ScreenPointToRay(Input.mousePosition);
                var hit = Physics.RaycastNonAlloc(point, _raycastHits, maxDistanceToCheck, layerMask);

                if (hit > 0 && _raycastHits[0].collider != null )
                {
                    _clickedObject = _raycastHits[0].collider.GetComponentInParent<Turret>();
                    _clickedObject.IsPicked(true);
                }
            }

            if (!_clickedObject || _camera == null) return;
            var zValue = _camera.WorldToScreenPoint(_clickedObject.transform.position).z;
            var position = new Vector3(Input.mousePosition.x, Input.mousePosition.y, zValue);
            var worldPosition = _camera.ScreenToWorldPoint(position);

            _clickedObject.transform.position = worldPosition;
        }
    }
}