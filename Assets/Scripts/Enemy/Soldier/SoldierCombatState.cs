using Enemy.AiContextInterfaces;

namespace Enemy.Soldier
{
    public class SoldierCombatState : EnemyFsmState
    {
        private readonly IAiLookAgent _lookAgent;
        private readonly IAiMoveAgent _moveAgent;
        private readonly IAiCombatAgent _combatAgent;
        private readonly IAiRepositionAgent _repositionAgent;

        public SoldierCombatState(HumanoidAiContext aiContext)
        {
            _lookAgent = aiContext;
            _moveAgent = aiContext;
            _combatAgent = aiContext;
            _repositionAgent = aiContext;
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
            else if (_repositionAgent.IsShelterPossible && !_repositionAgent.IsOnShelter
                     || !_combatAgent.CanAttackTarget)
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