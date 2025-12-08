using System;
using System.Collections;
using Camera;
using Character;
using Mech;
using Player;
using Unity.Cinemachine;
using UnityEngine;
using Zenject;

namespace InteractiveObjects
{
    [RequireComponent(typeof(ControlledEntityVirtualCameraContainer))]
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
        
        private ActiveCameraController _activeCameraController;
        private ControlledEntityVirtualCameraContainer _mountCameraContainerContainer;
        private ControlledEntityVirtualCameraContainer _riderVirtualCameraContainer;
        
        private bool isMounted = false;

        [Inject]
        private void Init(ActiveCameraController activeCameraController)
        {
            _activeCameraController = activeCameraController;
        }
        
        private void Awake()
        {
            _mountCameraContainerContainer = GetComponent<ControlledEntityVirtualCameraContainer>();
            
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
            if (isMounted)
                return;
            
            _rider = interactor;
            
            if (_rider.TryGetComponent(out _riderCharacterController))
                _riderCharacterController.enabled = false;
            _rider.transform.SetParent(transform);
            _rider.transform.position = transform.position + riderPositionOffset;
            
            if (_rider.TryGetComponent(out _playerInputController))
                _playerInputController.SetInputStrategy(_inputStrategy);

            _rider.TryGetComponent(out _riderVirtualCameraContainer);

            _activeCameraController.SetActiveCamera(_mountCameraContainerContainer.VirtualCamera);

            isMounted = true;
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

            _activeCameraController.SetActiveCamera(_riderVirtualCameraContainer.VirtualCamera);
            _riderVirtualCameraContainer = null;
            
            isMounted = false;
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