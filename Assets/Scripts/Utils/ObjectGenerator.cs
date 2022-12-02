using UnityEngine;

namespace Utils
{
    public class ObjectGenerator : MonoBehaviour
    {
        public Object InstantiateObject<T>(Object coroutineMethod)
        {
            System.Activator.CreateInstance<T>();
            return Instantiate(coroutineMethod) ;
        }
        
        public Object InstantiateObject<T>(Object coroutineMethod, Vector3 position, Quaternion quaternion)
        {
               var instance = Instantiate(coroutineMethod, position, quaternion);
               return instance;
        }
    }
}