using System;
using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        private IInputStrategy _inputStrategy;
        
        private IInputStrategy _defaultInputStrategy;
        
        private InputAction _attackInputAction;

        [Inject]
        public void Init(IInputStrategy inputStrategy)
        {
            _inputStrategy = _defaultInputStrategy = inputStrategy;
        }

        private void Awake()
        {
            _attackInputAction = InputSystem.actions.FindAction("Attack");
        }

        private void FixedUpdate()
        {
            _inputStrategy.CalculateAimTargetRequest();
            _inputStrategy.ChangeLookDirectionRequest();
        }

        private void Update()
        {
            SetAttackInput();
        }

        public void SetInputStrategy(IInputStrategy strategy)
        {
            _inputStrategy.ResetInputs();
            _inputStrategy = strategy;
            _inputStrategy.ResetInputs();
        }

        public void SetDefaultInputStrategy()
        {
            _inputStrategy.ResetInputs();
            _inputStrategy = _defaultInputStrategy;
            _inputStrategy.ResetInputs();
        }

        private void OnMove(InputValue value)
        {
            var vectorInput = value.Get<Vector2>();
            _inputStrategy.MoveRequest(vectorInput);
        }

        private void OnJump()
        {
            _inputStrategy.JumpRequest();
        }

        private void OnDash()
        {
            _inputStrategy.DashRequest();
        }

        private void OnInteract()
        {
            _inputStrategy.InteractRequest();
        }

        private void SetAttackInput()
        {
            _inputStrategy.SetAttackRequestStatus(_attackInputAction.IsPressed());
        }

        private void OnEquipment1()
        {
            _inputStrategy.ChoseEquipmentSlot1Request();
        }

        private void OnEquipment2()
        {
            _inputStrategy.ChoseEquipmentSlot2Request();
        }

        private void OnEquipment3()
        {
            _inputStrategy.ChoseEquipmentSlot3Request();
        }

        private void OnAbility1()
        {
            _inputStrategy.CastAbility1Request();
        }

        private void OnAbility2()
        {
            _inputStrategy.CastAbility2Request();
        }
    }
}