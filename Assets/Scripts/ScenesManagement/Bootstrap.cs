using UnityEngine;

namespace ScenesManagement
{
    public abstract class Bootstrap : MonoBehaviour
    {
        public abstract void Init(IBootstrapArgs args);
    }
}