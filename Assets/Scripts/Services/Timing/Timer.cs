using System;
using System.Collections;
using UnityEngine;

namespace Services.Timing
{
    public class Timer
    {
        public event Action OnTimerEnds;
        private float _remainingTime;
        public IEnumerator CountTime()
        {
            yield return new WaitForSeconds(_remainingTime);
            OnTimerEnds?.Invoke();
        }

        public void Init(float timeToWait)
        {
            _remainingTime = timeToWait;
        }
    }
}