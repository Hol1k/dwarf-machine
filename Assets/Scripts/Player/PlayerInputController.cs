using Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    [RequireComponent(typeof(CharacterMovementController))]
    [RequireComponent(typeof(CharacterMouseInputController))]
    public class PlayerInputController : MonoBehaviour
    {
        private IInputStrategy _inputStrategy;
        
        private CharacterInputStrategy _defaultCharacterInputStrategy;

        private void Awake()
        {
            CharacterMovementController characterMovementController = GetComponent<CharacterMovementController>();
            CharacterMouseInputController characterMouseInputController = GetComponent<CharacterMouseInputController>();
            
            _defaultCharacterInputStrategy = 
                new CharacterInputStrategy(characterMovementController, characterMouseInputController);
            SetDefaultInputStrategy();
        }

        public void SetInputStrategy(IInputStrategy strategy)
        {
            _inputStrategy = strategy;
        }

        public void SetDefaultInputStrategy()
        {
            _inputStrategy = _defaultCharacterInputStrategy;
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