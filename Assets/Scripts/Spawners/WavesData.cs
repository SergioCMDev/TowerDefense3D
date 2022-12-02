using System.Collections.Generic;
using UnityEngine;

namespace Spawners
{
    [CreateAssetMenu(fileName = "WavesData", menuName = "Waves/WavesData")]
    public class WavesData : ScriptableObject
    {
        public List<Wave> waves;
        public float timeBeforeFirstWaveStarts;
    }
}