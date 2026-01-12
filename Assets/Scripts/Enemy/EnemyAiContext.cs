using UnityEngine;

namespace Enemy
{
    public class EnemyAiContext
    {
        public EnemyAiContext(
            Transform enemyTransform,
            EnemyPatrolComponent patrolComponent,
            EnemyMoveController moveController,
            EnemyLookComponent lookComponent)
        {
            _enemyTransform = enemyTransform;
            _patrolComponent = patrolComponent;
            _moveController = moveController;
            _lookComponent = lookComponent;
        }

        public Vector3 EnemyPosition => _enemyTransform.position;
        private readonly Transform _enemyTransform;
        
        public bool IsSeeTarget => _lookComponent.IsSeeTarget;
        public Vector3? LastSeePosition => _lookComponent.LastSeePosition;
        public void ForgetLastSeePosition() => _lookComponent.ForgetLastSeePosition();
        private readonly EnemyLookComponent _lookComponent;
        
        public Vector3 NextPatrolPoint => _patrolComponent.GetNextPoint();
        private readonly EnemyPatrolComponent _patrolComponent;
        
        public void MoveTo(Vector3 position) => _moveController.MoveTo(position);
        public bool IsAgentArrivedToDestination() => _moveController.IsAgentArrivedToDestination();
        private readonly EnemyMoveController _moveController;
    }
}