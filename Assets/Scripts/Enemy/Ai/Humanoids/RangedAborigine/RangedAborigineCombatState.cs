using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai.Humanoids.RangedAborigine
{
    public class RangedAborigineCombatState : EnemyFsmState
    {
        private readonly IAiTransformAgent _transformAgent;
        private readonly IAiLookAgent _lookAgent;
        private readonly IAiMoveAgent _moveAgent;
        private readonly IAiCombatAgent _combatAgent;
        private readonly IAiAborigineTeamAgent _aborigineTeamAgent;

        private const float RepositionRange = 3f;

        public RangedAborigineCombatState(HumanoidAiContext aiContext)
        {
            _transformAgent = aiContext;
            _lookAgent = aiContext;
            _moveAgent = aiContext;
            _combatAgent = aiContext;
        }
        
        public override void Enter(EnemyFsmContext fsmContext)
        {
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (!_lookAgent.IsSeeTarget)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Alert;
            }
            else if (!_combatAgent.CanAttackTarget ||
                     _aborigineTeamAgent.IsAnyMeleeAlive &&
                     Vector3.Distance(_transformAgent.SelfPosition, _lookAgent.ClosestTarget.transform.position) < RepositionRange)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Reposition;
            }
            else if (!_combatAgent.IsTargetEliminated)
            {
                _moveAgent.LookAt(_lookAgent.ClosestTarget.transform.position);
                _combatAgent.AttackTarget(_lookAgent.ClosestTarget);
            }
            else
            {
                fsmContext.RequestedState = EnemyFsmStateId.Patrol;
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
        }
    }
}