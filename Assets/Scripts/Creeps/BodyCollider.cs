using System;
using UnityEngine;

namespace Creeps
{
    public class BodyCollider : MonoBehaviour
    {
        public Action<Collision> OnBodyCollide;

        private void OnCollisionEnter(Collision collision)
        {
            OnBodyCollide?.Invoke(collision);
        }
    }
}
