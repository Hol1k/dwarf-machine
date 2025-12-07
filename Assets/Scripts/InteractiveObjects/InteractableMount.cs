using System;
using System.Collections;
using Character;
using Mech;
using Player;
using Unity.Cinemachine;
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
        
        [Space]
        [SerializeField] private int cameraWeightId = 1;
        [SerializeField] private float cameraChangeAnimationDuration = 1;
        [SerializeField] private CinemachineMixingCamera cinemachineMixingCamera;

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

            StartCoroutine(SmoothChangeToMountCamera(cameraChangeAnimationDuration));
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

            StartCoroutine(SmoothChangeToRiderCamera(cameraChangeAnimationDuration));
        }

        private IEnumerator SmoothChangeToMountCamera(float duration)
        {
            float currentTime = 0;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                
                var smoothValue = currentTime / duration;
                cinemachineMixingCamera.Weight0 = 1 - smoothValue;
                cinemachineMixingCamera.SetWeight(cameraWeightId, smoothValue);
                
                yield return null;
            }
            
            cinemachineMixingCamera.Weight0 = 0;
            cinemachineMixingCamera.SetWeight(cameraWeightId, 1);
        }

        private IEnumerator SmoothChangeToRiderCamera(float duration)
        {
            float currentTime = 0;

            while (currentTime < duration)
            {
                currentTime += Time.deltaTime;
                
                var smoothValue = currentTime / duration;
                cinemachineMixingCamera.Weight0 = smoothValue;
                cinemachineMixingCamera.SetWeight(cameraWeightId, 1 - smoothValue);
                
                yield return null;
            }
            
            cinemachineMixingCamera.Weight0 = 1;
            cinemachineMixingCamera.SetWeight(cameraWeightId, 0);
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