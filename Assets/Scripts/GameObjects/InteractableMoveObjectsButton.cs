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
            
            _startPosition = target.localPosition;
            _startRotation = target.localRotation.eulerAngles;
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
                target.DOLocalMove(_startPosition + positionOffset, animationDuration);
                target.DOLocalRotate(_startRotation + rotationOffset, animationDuration);
                _isOnStart = false;
            }
            else
            {
                target.DOLocalMove(_startPosition, animationDuration);
                target.DOLocalRotate(_startRotation, animationDuration);
                _isOnStart = true;
            }
        }
    }
}