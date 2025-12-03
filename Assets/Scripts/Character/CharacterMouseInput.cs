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
        
        private Interactor _interactorComponent;

        private void Awake()
        {
            interactLabel.alpha = 0f;
            TryGetComponent(out _interactorComponent);
        }

        private void FixedUpdate()
        {
            CalculateTargetObject();
        }

        private void OnInteract()
        {
            if (_interactorComponent & _interactableTarget)
                _interactorComponent.Interact(_interactableTarget);
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