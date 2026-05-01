using UnityEngine;

namespace InteractiveObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public abstract void Interact(Interactor interactor);
    }
}