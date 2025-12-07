using System;
using Mech;
using Player;
using UnityEngine;

namespace InteractiveObjects
{
    public class InteractableMount : InteractableObject
    {
        [Space]
        [SerializeField] private Vector3 riderPositionOffset;
        [SerializeField] private Vector3 mountDownOffset;
        
        [SerializeField] private MountType mountType;
        private IInputStrategy _inputStrategy;
        
        private Interactor _rider;
        private CharacterController _riderCharacterController;
        private PlayerInputController _playerInputController;

        private void Awake()
        {
            InitInputStrategy();
        }

        private void OnValidate()
        {
            if (_rider)
                _rider.transform.position = transform.position + riderPositionOffset;

            InitInputStrategy();
        }

        public override void Interact(Interactor interactor)
        {
            _rider = interactor;
            
            _rider.TryGetComponent(out _riderCharacterController);
            if (_riderCharacterController)
                _riderCharacterController.enabled = false;
            _rider.transform.SetParent(transform);
            _rider.transform.position = transform.position + riderPositionOffset;
            
            _rider.TryGetComponent(out _playerInputController);
            if (_playerInputController)
                _playerInputController.SetInputStrategy(_inputStrategy);
        }

        public void MountDownRequest()
        {
            _rider.transform.SetParent(null);
            _rider.transform.position = transform.position + mountDownOffset;
            if (_riderCharacterController)
            {
                _riderCharacterController.enabled = true;
                _riderCharacterController = null;
            }
            if (_playerInputController)
            {
                _playerInputController.SetDefaultInputStrategy();
            }
            _rider.transform.localScale = Vector3.one;

            _rider = null;
        }

        private void InitInputStrategy()
        {
            switch (mountType)
            {
                case MountType.Mech:
                    _inputStrategy = new MechInputStrategy(this);
                    break;
            }
        }
    }
}