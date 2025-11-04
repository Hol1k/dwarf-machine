using UnityEngine;

namespace GameObjects
{
    public abstract class InteractableObject : MonoBehaviour
    {
        public virtual void Interact()
        {
            Debug.Log("Interacting with " + name);
        }
    }
}