using System;
using UnityEngine;

namespace InteractiveObjects
{
    public class InteractableMount : InteractableObject
    {
        [Space]
        [SerializeField] private Vector3 riderPositionOffset;
        
        private Transform _rider;

        private void OnValidate()
        {
            if (_rider)
                _rider.position = transform.position + riderPositionOffset;
        }

        public override void Interact(Interactor interactor)
        {
            _rider = interactor.transform;
            
            _rider.TryGetComponent(out CharacterController characterController);
            characterController.enabled = false;
            _rider.transform.SetParent(transform);
            _rider.transform.position = transform.position + riderPositionOffset;
        }
    }
}