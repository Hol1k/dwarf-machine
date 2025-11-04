using UnityEngine;

namespace InteractiveObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public virtual void Interact()
        {
            Debug.Log("Interacting with " + name);
        }
    }
}