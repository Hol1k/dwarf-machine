using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai.SilverSwarm.SilverEnemy
{
    public class SilverEnemyCombatState : EnemyFsmState
    {
        private readonly IAiSwarmDataAgent _swarmDataAgent;
        private readonly IAiTransformAgent _transformAgent;
        private readonly IAiMoveAgent _moveAgent;
        
        public SilverEnemyCombatState(SilverEnemyAiContext aiContext)
        {
            _swarmDataAgent = aiContext;
            _transformAgent = aiContext;
            _moveAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (!_swarmDataAgent.AttackFlag)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Patrol;
            }
            else if (!fsmContext.RepositionPoint.HasValue ||
                     (_swarmDataAgent.TargetPosition.HasValue &&
                      Vector3.Distance(_swarmDataAgent.TargetPosition.Value, fsmContext.RepositionPoint.Value) > 1f))
            {
                fsmContext.RepositionPoint = _swarmDataAgent.GetPointBehindTarget;
            }
            else
            {
                _moveAgent.MoveTo(fsmContext.RepositionPoint.Value);
                if (!_moveAgent.IsAgentArrivedToDestination)
                {
                    _moveAgent.MoveTo(fsmContext.RepositionPoint.Value);
                }
                else
                {
                    fsmContext.RepositionPoint = null;
                }
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
            _moveAgent.StopMove();
            fsmContext.RepositionPoint = null;
        }
    }
}