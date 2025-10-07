using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Character
{
    public class CharacterMovement : MonoBehaviour
    {
        private CharacterController _controller;
        [SerializeField] private Transform transformCamera;
        
        [NonSerialized] public float MoveSpeed;
        [NonSerialized] public float JumpHeight;
        [NonSerialized] public float DashRange;
        [NonSerialized] public float DashCooldown;
        [NonSerialized] public float DashDuration;

        [SerializeField] private float turnSmoothTime = 0.1f; // регулирует плавность разворота

        private bool _isGrounded;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.3f;
        [SerializeField] private LayerMask groundMask;
        private Vector3 _velocity; // воздействие гравитации
        [SerializeField] private float gravity = -9.81f;
        
        private Vector2 _vectorInput;
        private Vector3 _vectorMove;
        private float _jumpForce;
        private Vector3 _cameraForward;
        private Vector3 _cameraRight;
        private float _turnSmoothVelocity;
        
        private float _dashCurrCooldown;
        private Vector3 _dashVector;
        private bool _isDashing;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        public void OnMove(InputValue value)
        {      
            _vectorInput = value.Get<Vector2>();
        }

        public void OnJump()
        {
            if (_isGrounded)
            {
                _jumpForce = Mathf.Sqrt(2 * -gravity * JumpHeight);
                _velocity.y = _jumpForce;
            }
        }

        public void OnDash()
        {
            if (_dashCurrCooldown <= 0f)
            {
                _isDashing = true;
                _dashCurrCooldown = DashCooldown;
                _dashVector = _vectorMove.normalized * DashRange;
                
                Sequence sequence = DOTween.Sequence();
                sequence.AppendInterval(DashDuration);
                sequence.OnComplete(() =>
                {
                    _isDashing = false;
                    _velocity.y = -2f;
                });
            }
        }

        private void Update()
        {
            CalculateDashCooldown();
        }

        private void FixedUpdate()
        {
            GroundCheck();
            CalculateMoveVector();
            LookCharacterForward();

            ApplyMovement();
        }

        private void GroundCheck()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
        }

        private void CalculateMoveVector()
        {
            _cameraForward = transformCamera.forward;
            _cameraRight = transformCamera.right;

            _cameraForward.y = 0f;
            _cameraRight.y = 0f;

            _cameraForward.Normalize();
            _cameraRight.Normalize();

            _vectorMove = (_cameraForward * _vectorInput.y * MoveSpeed) + (_cameraRight * _vectorInput.x * MoveSpeed);
        }

        private void LookCharacterForward()
        {
            Vector3 directionLook = transform.position - transformCamera.position;
            directionLook.y = 0f;

            if (directionLook.sqrMagnitude > 0.01f)
            {
                float targetAngle = Mathf.Atan2(directionLook.x, directionLook.z) * Mathf.Rad2Deg;
                float smoothedAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref _turnSmoothVelocity, turnSmoothTime);
                transform.rotation = Quaternion.Euler(0f, smoothedAngle, 0f);
            }
        }

        private void ApplyMovement()
        {
            if (_isDashing)
            {
                var dashSpeed = _dashVector / DashDuration;
                _controller.Move(dashSpeed * Time.fixedDeltaTime);
            }
            else
                _controller.Move(_vectorMove * Time.fixedDeltaTime);

            // Гравитация
            _velocity.y += gravity * Time.fixedDeltaTime;
            _controller.Move(_velocity * Time.fixedDeltaTime);
        }

        private void CalculateDashCooldown()
        {
            _dashCurrCooldown -= Time.deltaTime;
        }
    }
}
