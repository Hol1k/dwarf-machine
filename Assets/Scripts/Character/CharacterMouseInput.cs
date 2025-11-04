using InteractiveObjects;
using UnityEngine;

namespace Character
{
    public class CharacterMouseInput : MonoBehaviour
    {
        private GameObject _lookTarget;
        private InteractableObject _interactableTarget;
        
        [SerializeField] private Transform playerLookTransform;
        
        [Space]
        [SerializeField] private CanvasGroup interactLabel;
        [SerializeField] private float interactDistance = 20f;

        private void Awake()
        {
            interactLabel.alpha = 0f;
        }

        private void FixedUpdate()
        {
            CalculateTargetObject();
        }

        public void OnInteract()
        {
            if (_interactableTarget)
                _interactableTarget.Interact();
        }
        
        private void CalculateTargetObject()
        {
            if (Physics.Raycast(playerLookTransform.position, playerLookTransform.forward,
                    out RaycastHit hitInfo, interactDistance)) 
            {
                if (hitInfo.collider.gameObject != _lookTarget) // if same object, don't change target
                {
                    _lookTarget = hitInfo.transform.gameObject;
                    hitInfo.collider.TryGetComponent(out InteractableObject newInteractable);
                    _interactableTarget = newInteractable;
                    interactLabel.alpha = _interactableTarget ? 1f : 0f;
                }
            }
            else
            {
                _lookTarget = null;
                _interactableTarget = null;
                interactLabel.alpha = 0f;
            }
        }
    }
}