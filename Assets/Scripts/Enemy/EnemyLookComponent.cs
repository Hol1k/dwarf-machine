using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyLookComponent : MonoBehaviour
    {
        public bool IsSeeTarget { get; private set; }
        public Vector3? LastSeePosition { get; private set; }
        
        private readonly List<GameObject> _visibleObjects = new();
        [SerializeField] private LayerMask targetLayerMask;
        [SerializeField] private LayerMask obstaclesLayerMask;

        private void Update()
        {
            DetectTarget();
        }
        
        public void ForgetLastSeePosition() => LastSeePosition = null;

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
                        IsSeeTarget = true;
                }
            }
            
            if (IsSeeTarget)
                LastSeePosition = _visibleObjects[0].transform.position;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (1 << other.gameObject.layer == targetLayerMask.value)
            {
                _visibleObjects.Add(other.gameObject);
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (_visibleObjects.Contains(other.gameObject))
            {
                _visibleObjects.Remove(other.gameObject);
            }
        }
    }
}