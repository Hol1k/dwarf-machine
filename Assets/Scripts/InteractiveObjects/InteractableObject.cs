using UnityEngine;

namespace InteractiveObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public virtual void Interact(Interactor interactor)
        {
            Debug.Log("Interacting with " + name);
        }
    }
}