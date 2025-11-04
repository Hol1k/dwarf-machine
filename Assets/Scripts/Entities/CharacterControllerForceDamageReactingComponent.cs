using System;
using UnityEngine;

namespace Entities
{
    [RequireComponent(typeof(CharacterController))]
    public class CharacterControllerForceDamageReactingComponent : MonoBehaviour
    {
        private CharacterController _controller;
        private bool _isGrounded;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.3f;
        [SerializeField] private LayerMask groundMask;
        private Vector3 _forceRequest;
        private Vector3 _velocity;
        [SerializeField] private float gravity = -9.81f;

        private void Awake()
        {
            _controller = GetComponent<CharacterController>();
        }

        public void AddKnockbackForce(Vector3 force)
        {
            _forceRequest += new Vector3(force.x, Mathf.Sqrt(2 * -gravity * force.y), force.z);
        }

        private void FixedUpdate()
        {
            SetVelocity();
            GroundCheck();

            ApplyMovement();
        }

        private void SetVelocity()
        {
            _velocity += _forceRequest;
            _forceRequest = Vector3.zero;
        }

        private void GroundCheck()
        {
            _isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) && _velocity.y <= 0f;
            if (_isGrounded && _velocity.y < 0)
            {
                _velocity.y = -2f;
            }
        }

        private void ApplyMovement()
        {
            // Гравитация
            _velocity.y += gravity * Time.fixedDeltaTime;
            _controller.Move(_velocity * Time.fixedDeltaTime);

            if (_isGrounded)
            {
                _velocity.x = 0f;
                _velocity.z = 0f;
            }
        }
    }
}