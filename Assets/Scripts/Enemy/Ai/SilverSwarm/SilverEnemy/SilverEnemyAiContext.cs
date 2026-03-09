using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai.SilverSwarm.SilverEnemy
{
    public class SilverEnemyAiContext : EnemyAiContext, IAiSwarmDataAgent, IAiMoveAgent
    {
        public SilverEnemyAiContext(
            SilverSwarmAiContext dataAgent,
            EnemyMoveController moveController
        )
        {
            _dataAgent = dataAgent;
            _moveController = moveController;
        }

        public bool AttackFlag => _dataAgent.AttackFlag;
        public bool IsPointInsideSwarm(Vector3 point) => _dataAgent.IsPointInsideSwarm(point);
        public Vector3 GetPointInsideSwarm => _dataAgent.GetPointInsideSwarm;
        private readonly IAiSwarmDataAgent _dataAgent;
        
        public void MoveTo(Vector3 position) => _moveController.MoveTo(position);
        public void StopMove() => _moveController.StopMove();
        public void LookAt(Vector3 target) => _moveController.LookAt(target);
        public bool IsAgentArrivedToDestination => _moveController.IsAgentArrivedToDestination();
        private readonly EnemyMoveController _moveController;
    }
}