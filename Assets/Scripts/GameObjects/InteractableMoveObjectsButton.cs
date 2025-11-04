using DG.Tweening;
using UnityEngine;

namespace GameObjects
{
    public class InteractableMoveObjectsButton : InteractableObject
    {
        private Vector3 _startPosition;
        private Vector3 _startRotation;
        private bool _isOnStart = true;
        
        [SerializeField] private Transform target;
        
        [SerializeField] private Vector3 positionOffset;
        [SerializeField] private Vector3 rotationOffset;
        [SerializeField] [Min(0f)] private float animationDuration = 1f;

        private void Start()
        {
            if (!target) return;
            
            _startPosition = target.position;
            _startRotation = Vector3.zero;
        }

        public override void Interact()
        {
            if (!target)
            {
                Debug.LogError("No target for interaction");
                return;
            }

            if (_isOnStart)
            {
                target.DOMove(_startPosition + positionOffset, animationDuration);
                target.DORotate(rotationOffset, animationDuration);
                _isOnStart = false;
            }
            else
            {
                target.DOMove(_startPosition, animationDuration);
                target.DORotate(_startRotation, animationDuration);
                _isOnStart = true;
            }
        }
    }
}