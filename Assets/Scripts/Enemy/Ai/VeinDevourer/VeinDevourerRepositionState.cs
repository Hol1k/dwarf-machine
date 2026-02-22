using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai.VeinDevourer
{
    public class VeinDevourerRepositionState : EnemyFsmState
    {
        private readonly IAiLookAgent _lookAgent;
        private readonly IAiCombatAgent _combatAgent;
        private readonly IAiMoveAgent _moveAgent;
        private readonly IAiTransformAgent _transformAgent;

        public VeinDevourerRepositionState(VeinDevourerAiContext aiContext)
        {
            _lookAgent = aiContext;
            _combatAgent = aiContext;
            _moveAgent = aiContext;
            _transformAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            var targetPos = new Vector2(_lookAgent.ClosestTarget.transform.position.x, _lookAgent.ClosestTarget.transform.position.z);
            var selfPos = new Vector2(_transformAgent.SelfPosition.x, _transformAgent.SelfPosition.z);
            if (!_lookAgent.IsSeeTarget)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Idle;
            }
            else if (_lookAgent.ClosestTargetInventoryValue > 0.6f && _combatAgent.CanAttackTarget)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Combat;
            }
            else if (_lookAgent.ClosestTargetInventoryValue > 0.6f && !_combatAgent.CanAttackTarget)
            {
                _moveAgent.MoveTo(_lookAgent.ClosestTarget.transform.position);
            }
            else if (_lookAgent.ClosestTargetInventoryValue > 0.3f &&
                     Vector2.Distance(targetPos, selfPos) > 5f)
            {
                _moveAgent.MoveTo(_lookAgent.ClosestTarget.transform.position);
            }
            else
            {
                fsmContext.RequestedState = EnemyFsmStateId.Idle;
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
            _moveAgent.StopMove();
        }
    }
}