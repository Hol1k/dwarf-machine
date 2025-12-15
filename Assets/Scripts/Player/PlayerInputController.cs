using UnityEngine;
using UnityEngine.InputSystem;
using Zenject;

namespace Player
{
    public class PlayerInputController : MonoBehaviour
    {
        private IInputStrategy _inputStrategy;
        
        private IInputStrategy _defaultInputStrategy;

        [Inject]
        public void Init(IInputStrategy inputStrategy)
        {
            _inputStrategy = _defaultInputStrategy = inputStrategy;
        }

        private void FixedUpdate()
        {
            _inputStrategy.CalculateAimTargetRequest();
            _inputStrategy.ChangeLookDirectionRequest();
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
    }
}