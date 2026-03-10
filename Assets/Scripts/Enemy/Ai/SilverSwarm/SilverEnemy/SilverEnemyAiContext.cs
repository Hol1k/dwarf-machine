using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai.SilverSwarm.SilverEnemy
{
    public class SilverEnemyAiContext : EnemyAiContext, IAiSwarmDataAgent, IAiMoveAgent, IAiTransformAgent
    {
        public SilverEnemyAiContext(
            SilverSwarmAiContext dataAgent,
            EnemyMoveController moveController,
            Transform selfTransform)
        {
            _dataAgent = dataAgent;
            _moveController = moveController;
            _selfTransform = selfTransform;
        }

        public bool AttackFlag => _dataAgent.AttackFlag;
        public bool IsPointInsideSwarm(Vector3 point) => _dataAgent.IsPointInsideSwarm(point);
        public Vector3 GetPointInsideSwarm => _dataAgent.GetPointInsideSwarm;
        public Vector3? TargetPosition => _dataAgent.TargetPosition;
        public Vector3? GetPointBehindTarget => _dataAgent.GetPointBehindTarget;
        private readonly IAiSwarmDataAgent _dataAgent;
        
        public void MoveTo(Vector3 position) => _moveController.MoveTo(position);
        public void StopMove() => _moveController.StopMove();
        public void LookAt(Vector3 target) => _moveController.LookAt(target);
        public bool IsAgentArrivedToDestination => _moveController.IsAgentArrivedToDestination();
        private readonly EnemyMoveController _moveController;
        
        public Vector3 SelfPosition => _selfTransform.position;
        private readonly Transform _selfTransform;
    }
}