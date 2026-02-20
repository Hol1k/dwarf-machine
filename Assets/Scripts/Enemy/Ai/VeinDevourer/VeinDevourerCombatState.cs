using Enemy.Ai.AiContextInterfaces;

namespace Enemy.Ai.VeinDevourer
{
    public class VeinDevourerCombatState : EnemyFsmState
    {
        private readonly IAiCombatAgent _combatAgent;
        private readonly IAiLookAgent _lookAgent;
        
        public VeinDevourerCombatState(VeinDevourerAiContext aiContext)
        {
            _combatAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_combatAgent.IsTargetEliminated)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Idle;
            }
            else if (_combatAgent.CanAttackTarget)
            {
                _combatAgent.AttackTarget(_lookAgent.ClosestTarget);
            }
            else
            {
                fsmContext.RequestedState = EnemyFsmStateId.Reposition;
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
        }
    }
}