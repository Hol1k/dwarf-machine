using Modifiers;
using UnityEngine;

namespace Mech
{
    public class MechMovementController : MonoBehaviour
    {
        private Rigidbody _rigidbody;
        [SerializeField] private Transform transformCamera;
        
        [SerializeField] public float MoveSpeed; // need to make config init and make NonSerialize

        [SerializeField] private float turnSmoothTime = 0.1f; // регулирует плавность разворота
        
        private Vector2 _vectorInput;
        private Vector3 _moveVector;
        private float _turnSmoothVelocity;

        private ModifierHandler _modifierHandler;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            _modifierHandler = GetComponent<ModifierHandler>();
        }

        public void ResetInputs()
        {
            //reset movement
            _vectorInput = Vector3.zero;
        }

        public void SetMoveVector(Vector2 movementVector)
        {
            _vectorInput = _modifierHandler.ModifyMovement(movementVector);
        }

        public void LookMechForward()
        {
            Vector3 directionLook = transformCamera.forward;
            directionLook.y = 0f;

            if (directionLook.sqrMagnitude > 0.01f)
            {
                float targetAngle = Mathf.Atan2(directionLook.x, directionLook.z) * Mathf.Rad2Deg;
                float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
            }
        }

        private void FixedUpdate()
        {
            CalculateMoveVector();

            ApplyMovement();
        }
        
        private void CalculateMoveVector()
        {
            var cameraForward = transformCamera.forward;
            var cameraRight = transformCamera.right;

            cameraForward.y = 0f;
            cameraRight.y = 0f;

            cameraForward.Normalize();
            cameraRight.Normalize();

            _moveVector = cameraForward * (_vectorInput.y * MoveSpeed) + cameraRight * (_vectorInput.x * MoveSpeed);
        }

        private void ApplyMovement()
        {
            _rigidbody.MovePosition(transform.position + _moveVector * Time.fixedDeltaTime);
        }
    }
}