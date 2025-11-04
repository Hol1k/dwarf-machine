using System;
using Entities;
using UnityEngine;

namespace Traps
{
    [RequireComponent(typeof(SphereCollider))]
    public class SpikedMine : MonoBehaviour
    {
        private SphereCollider _collider;
        
        [SerializeField] private bool developmentMode = false;
        [SerializeField] private Color explosionRadiusColor = new Color(1f, 0f, 0f, 0.2f);
        
        [Space]
        [SerializeField] [Min(0f)] private float activationRadius;
        [SerializeField] [Min(0f)] private float explosionRadius;
        [SerializeField] [Min(0f)] private float damage;
        
        [Space]
        [SerializeField] private LayerMask targetMask;
        [SerializeField] private LayerMask damageMask;

        private void Awake()
        {
            _collider = GetComponent<SphereCollider>();
        }

        private void Start()
        {
            _collider.radius = activationRadius;
        }

        private void OnTriggerEnter(Collider other)
        {
            Explode(other.gameObject);
        }

        private void Explode(GameObject other)
        {
            if (((1 << other.gameObject.layer) & targetMask) != 0)
            {
                foreach (var hitCollider in Physics.OverlapSphere(transform.position, explosionRadius, damageMask))
                {
                    if (hitCollider.TryGetComponent(out IDamageable damageable))
                        damageable.TakeDamage(damage);
                }
                
                Destroy(gameObject);
            }
        }

        private void OnDrawGizmos()
        {
            if (developmentMode)
            {
                Gizmos.color = explosionRadiusColor;
                
                Gizmos.DrawSphere(transform.position, explosionRadius);
            }
        }

        private void OnValidate()
        {
            GetComponent<SphereCollider>().radius = activationRadius;
        }
    }
}