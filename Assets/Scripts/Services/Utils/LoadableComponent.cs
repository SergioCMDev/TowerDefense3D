using Services.Interfaces;
using UnityEngine;

namespace Services.Utils
{
    public abstract class LoadableComponent : ScriptableObject, ILoadable
    {
        public abstract void Execute();
    }
}