using System;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Entities
{
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Rigidbody))]
    public class NavMeshAgentForceDamageReactingComponent : MonoBehaviour, IForceDamageReactingComponent
    {
        private NavMeshAgent _agent;
        private Rigidbody _rigidbody;
        
        private Vector3 _forceRequest;
        private bool IsOnForce => !_agent.enabled;
        [SerializeField] private Transform groundCheck;
        [SerializeField] private float groundDistance = 0.3f;
        [SerializeField] private LayerMask groundMask;

        private const float Gravity = -9.81f;

        [Inject]
        private void Init(NavMeshAgent agent, Rigidbody rb)
        {
            _agent = agent;
            _rigidbody = rb;
        }

        public void AddKnockbackForce(Vector3 force)
        {
            _forceRequest += new Vector3(force.x, Mathf.Sqrt(2 * -Gravity * force.y), force.z);
        }

        private void FixedUpdate()
        {
            SetForce();
            GroundCheck();
        }

        private void SetForce()
        {
            if (!_forceRequest.Equals(Vector3.zero))
            {
                _agent.enabled = false;
                _rigidbody.useGravity = true;
                _rigidbody.isKinematic = false;
                _rigidbody.linearVelocity = _forceRequest;
                
                _forceRequest = Vector3.zero;
            }
        }

        private void GroundCheck()
        {
            if (IsOnForce)
            {
                var isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask) &&
                              _rigidbody.linearVelocity.y <= 0f;
            
                if (isGrounded)
                {
                    _agent.enabled = true;
                    _rigidbody.useGravity = false;
                    _rigidbody.isKinematic = true;
                }
            }
        }
    }
}