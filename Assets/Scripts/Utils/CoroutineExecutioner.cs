using System.Collections;
using UnityEngine;

namespace Utils
{
    public class CoroutineExecutioner : MonoBehaviour
    {
        public void StartChildCoroutine(IEnumerator coroutineMethod)
        {
            StartCoroutine(coroutineMethod);
        }
    }
}