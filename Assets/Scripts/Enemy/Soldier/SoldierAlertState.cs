using Enemy.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Soldier
{
    public class SoldierAlertState : EnemyFsmState
    {
        private readonly IAiLookAgent _lookAgent;
        private readonly IAiMoveAgent _moveAgent;

        public SoldierAlertState(HumanoidAiContext aiContext)
        {
            _lookAgent = aiContext;
            _moveAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
            fsmContext.LookingTimer = 5f;
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_lookAgent.IsSeeTarget)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Combat;
            }
            else if (_lookAgent.LastSeePosition == null)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Patrol;
            }
            else
            {
                _moveAgent.MoveTo(_lookAgent.LastSeePosition.Value);
                if (!_moveAgent.IsAgentArrivedToDestination)
                {
                    _moveAgent.MoveTo(_lookAgent.LastSeePosition.Value);
                }
                else if (fsmContext.LookingTimer >= 0f)
                {
                    fsmContext.LookingTimer -= Time.deltaTime;
                }
                else
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Patrol;
                }
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
            _moveAgent.StopMove();
        }
    }
}