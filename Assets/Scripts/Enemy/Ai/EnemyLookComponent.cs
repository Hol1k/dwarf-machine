using System.Collections.Generic;
using Character;
using UnityEngine;
using Zenject;

namespace Enemy.Ai
{
    public class EnemyLookComponent : MonoBehaviour
    {
        public bool IsSeeTarget { get; private set; }
        public Vector3? LastSeePosition { get; private set; }
        
        public float LookRange => _lookSphereCollider.radius;
        private SphereCollider _lookSphereCollider;

        private readonly List<CharacterStatsComponent> _visibleObjects = new();
        [SerializeField] private LayerMask obstaclesLayerMask;

        [Inject]
        private void Init(SphereCollider lookSphereCollider)
        {
            _lookSphereCollider = lookSphereCollider;
        }
        
        private void Update()
        {
            DetectTarget();
        }
        
        public void ForgetLastSeePosition() => LastSeePosition = null;

        public bool IsSeeTargetFrom(Vector3 position)
        {
            if (_visibleObjects.Count == 0)
                return false;
            
            foreach (var obj in _visibleObjects)
            {
                var obstacles = Physics.OverlapCapsule(position,
                    obj.transform.position, 0.05f, obstaclesLayerMask);
                    
                if (obstacles.Length == 0)
                    return true;
            }
            
            return false;
        }

        public CharacterStatsComponent GetClosestTarget()
        {
            CharacterStatsComponent closestTarget = null;
            var closestDistance = float.MaxValue;
            
            foreach (var target in _visibleObjects)
            {
                if (!closestTarget || Vector3.Distance(transform.position, target.transform.position) < closestDistance)
                {
                    closestDistance = Vector3.Distance(transform.position, target.transform.position);
                    closestTarget = target;
                }
            }
            
            return closestTarget;
        }

        private void DetectTarget()
        {
            IsSeeTarget = false;

            if (_visibleObjects.Count > 0)
            {
                foreach (var obj in _visibleObjects)
                {
                    var obstacles = Physics.OverlapCapsule(transform.position,
                        obj.transform.position, 0.05f, obstaclesLayerMask);
                    
                    if (obstacles.Length == 0)
                    {
                        IsSeeTarget = true;
                        LastSeePosition = obj.transform.position;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out CharacterStatsComponent characterStats) && !characterStats.IsDied)
            {
                _visibleObjects.Add(characterStats);
                characterStats.OnDeath += () => _visibleObjects.Remove(characterStats);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out CharacterStatsComponent characterStats)) return;
            
            if (_visibleObjects.Contains(characterStats))
            {
                _visibleObjects.Remove(characterStats);
            }
        }
    }
}