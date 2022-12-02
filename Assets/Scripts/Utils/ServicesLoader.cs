using System.Collections.Generic;
using System.Linq;
using Services.Interfaces;
using UnityEngine;

namespace Utils
{
    public class ServicesLoader : MonoBehaviour
    {
        [SerializeField] private List<ScriptableObject> loadablesSO;
        private Dictionary<ILoadable, bool> _loadableStatus;

        private void Awake()
        {
            _loadableStatus = new Dictionary<ILoadable, bool>();
            ExecuteComponents(loadablesSO);
        }

        private void ExecuteComponents(List<ScriptableObject> list)
        {
            var loadables = new List<ILoadable>();
            foreach (var loadable in list)
            {
                var component = (ILoadable)loadable;
                loadables.Add(component);
            }

            foreach (var loadable in loadables)
            {
                if (_loadableStatus.ContainsKey(loadable) && _loadableStatus[loadable]) continue;
 
                loadable.Execute();

                _loadableStatus.Add(loadable, true);
                var objectType = loadable.GetType();
                if (objectType.GetInterfaces().Any(x => x != typeof(ILoadable)))
                {
                    var interfaceRetrieved = objectType.GetInterfaces().Single(x => x != typeof(ILoadable));
                    ServiceLocator.Instance.RegisterService(interfaceRetrieved, loadable);
                    continue;
                }
                ServiceLocator.Instance.RegisterService(objectType, loadable);
            }
        }
    }
}