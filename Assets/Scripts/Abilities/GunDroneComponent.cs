using System.Collections.Generic;
using Entities;
using Unity.VisualScripting;
using UnityEngine;

namespace Abilities
{
    public class GunDroneComponent : MonoBehaviour
    {
        public float attackSpeed;
        public float damage;
        [SerializeField] private LayerMask targetMask;

        public Transform droneHandler;
        [SerializeField] Vector3 positionOffset;

        private readonly List<GameObject> _visibleTargets = new();

        private float _lastAttackTime = 0;

        private void Update()
        {
            var closestTarget = CalculateClosestTarget();

            FollowToHandler();
            
            if (!closestTarget)
            {
                transform.rotation = droneHandler.rotation;
                return;
            }
            transform.LookAt(closestTarget.transform);
            if (Time.time - _lastAttackTime > 60 / attackSpeed &&
                closestTarget.TryGetComponent(out IDamageable damageable))
            {
                damageable.TakeDamage(damage);
                _lastAttackTime = Time.time;
            }
        }

        private void FollowToHandler()
        {
            transform.position = Vector3.Lerp(transform.position, droneHandler.position + droneHandler.rotation * positionOffset, 0.2f);
        }

        private GameObject CalculateClosestTarget()
        {
            var closestDistance = float.MaxValue;
            GameObject closestTarget = null;
            for (int i = _visibleTargets.Count - 1; i >= 0; i--)
            {
                if (_visibleTargets[i].IsDestroyed())
                {
                    _visibleTargets.Remove(_visibleTargets[i]);
                    continue;
                }

                var currDistance = Vector3.Distance(transform.position, _visibleTargets[i].transform.position);
                if (!closestTarget ||
                    currDistance < closestDistance)
                {
                    closestTarget = _visibleTargets[i];
                    closestDistance = currDistance;
                }
            }
            
            return closestTarget;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (1 << other.gameObject.layer == targetMask.value)
            {
                _visibleTargets.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            _visibleTargets.Remove(other.gameObject);
        }
    }
}