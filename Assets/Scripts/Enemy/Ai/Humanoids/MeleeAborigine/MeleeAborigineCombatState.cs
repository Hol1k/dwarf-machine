using Enemy.Ai.AiContextInterfaces;

namespace Enemy.Ai.Humanoids.MeleeAborigine
{
    public class MeleeAborigineCombatState : EnemyFsmState
    {
        private readonly IAiLookAgent _lookAgent;
        private readonly IAiMoveAgent _moveAgent;
        private readonly IAiCombatAgent _combatAgent;

        public MeleeAborigineCombatState(HumanoidAiContext aiContext)
        {
            _lookAgent = aiContext;
            _moveAgent = aiContext;
            _combatAgent = aiContext;
        }
        
        public override void Enter(EnemyFsmContext fsmContext)
        {
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (!_lookAgent.IsSeeTarget && !_combatAgent.IsTargetEliminated)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Alert;
            }
            else if (!_combatAgent.CanAttackTarget)
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