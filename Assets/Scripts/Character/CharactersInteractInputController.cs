using InteractiveObjects;
using Mech;
using UnityEngine;

namespace Character
{
    public class CharactersInteractInputController : MonoBehaviour
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

        public void InteractRequest()
        {
            if (_interactorComponent)
            {
                if (_interactableTarget)
                {
                    TryMoveInventoryItems(_interactableTarget);
                    _interactorComponent.Interact(_interactableTarget);
                }
            }
            else
                Debug.Log($"Interactor component not found on {gameObject.name} object");
        }

        private void TryMoveInventoryItems(InteractableObject interactableObject)
        {
            if (interactableObject.TryGetComponent(out MechInventoryComponent mechInventoryComponent)
                && TryGetComponent(out CharacterInventoryComponent charInventoryComponent))
            {
                foreach (var slot in charInventoryComponent.TakeAllLoot())
                {
                    mechInventoryComponent.AddLoot(slot.Key, slot.Value);
                }
            }
        }
        
        public void CalculateTargetObject()
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

        public void ResetTargets()
        {
            _lookTarget = null;
            _interactableTarget = null;
            interactLabel.alpha = 0f;
        }
    }
}