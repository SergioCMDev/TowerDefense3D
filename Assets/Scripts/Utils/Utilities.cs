
using UnityEngine;

namespace Utils
{
    public static class Utilities
    {
        public static int GetRandomValue(int min, int max)
        {
            return Random.Range(min, max);
        }
        
        public static bool ObjectsAreClose(Vector3 positionA, Vector3 positionB, float distanceToBeConsideredClose)
        {
            // Debug.Log($"Distance {Vector3.Distance(positionA, positionB)}");
            return Vector3.Distance(positionA, positionB) <= distanceToBeConsideredClose;
        }
    }
}