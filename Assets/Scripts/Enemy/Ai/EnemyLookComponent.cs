using System;
using System.Collections.Generic;
using Entities;
using Loot;
using Mech;
using UnityEngine;
using Zenject;

namespace Enemy.Ai
{
    public class EnemyLookComponent : MonoBehaviour
    {
        public bool IsSeeTarget => _visibleObjects.Count > 0;
        public Vector3? LastSeePosition { get; private set; }

        public IReadOnlyDictionary<LootType, float> ClosestTargetInventoryValue => GetClosestTarget() is IInventoryData inventoryData
            ? inventoryData.Loot
            : new Dictionary<LootType, float>();
        public float LookRange => _lookSphereCollider.radius;

        private SphereCollider _lookSphereCollider;

        private readonly List<StatsComponent> _visibleObjects = new();
        [SerializeField] private LayerMask obstaclesLayerMask;
        [SerializeField] private LayerMask targetLayerMask;

        private readonly Dictionary<StatsComponent, Action<GameObject>> _onDeathActionHandlersCollection = new();

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

        public StatsComponent GetClosestTarget()
        {
            StatsComponent closestTarget = null;
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
            if (_visibleObjects.Count > 0)
            {
                foreach (var obj in _visibleObjects)
                {
                    var obstacles = Physics.OverlapCapsule(transform.position,
                        obj.transform.position, 0.05f, obstaclesLayerMask);
                    
                    if (obstacles.Length == 0)
                    {
                        LastSeePosition = obj.transform.position;
                    }
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out StatsComponent targetStats) && !targetStats.IsDied &&
                (targetLayerMask >> targetStats.gameObject.layer) % 2 == 1)
            {
                _visibleObjects.Add(targetStats);
                _onDeathActionHandlersCollection.Add(
                    targetStats,
                    _ =>
                    {
                        if (_visibleObjects.Contains(targetStats))
                            _visibleObjects.Remove(targetStats);
                        
                        targetStats.OnDeath -= _onDeathActionHandlersCollection[targetStats];
                        _onDeathActionHandlersCollection.Remove(targetStats);
                    });
                targetStats.OnDeath += _onDeathActionHandlersCollection[targetStats];
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.TryGetComponent(out StatsComponent targetStats)) return;
            
            if (_visibleObjects.Contains(targetStats))
            {
                _visibleObjects.Remove(targetStats);
            }

            if (_onDeathActionHandlersCollection.ContainsKey(targetStats))
            {
                targetStats.OnDeath -= _onDeathActionHandlersCollection[targetStats];
                _onDeathActionHandlersCollection.Remove(targetStats);
            }
        }
    }
}