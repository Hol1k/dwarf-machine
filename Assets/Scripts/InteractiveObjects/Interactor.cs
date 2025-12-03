using UnityEngine;

namespace InteractiveObjects
{
    public class Interactor : MonoBehaviour
    {
        public void Interact(InteractableObject interactable)
        {
            interactable.Interact(this);
        }
    }
}