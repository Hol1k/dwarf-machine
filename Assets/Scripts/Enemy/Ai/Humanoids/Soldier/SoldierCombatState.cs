using Enemy.Ai;
using Enemy.Ai.AiContextInterfaces;
using Enemy.Ai.Humanoids;

namespace Enemy.Humanoids.Soldier
{
    public class SoldierCombatState : EnemyFsmState
    {
        private readonly IAiLookAgent _lookAgent;
        private readonly IAiMoveAgent _moveAgent;
        private readonly IAiCombatAgent _combatAgent;
        private readonly IAiShelterRepositionAgent _shelterRepositionAgent;

        public SoldierCombatState(HumanoidAiContext aiContext)
        {
            _lookAgent = aiContext;
            _moveAgent = aiContext;
            _combatAgent = aiContext;
            _shelterRepositionAgent = aiContext;
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
            else if (_shelterRepositionAgent.IsShelterPossible && !_shelterRepositionAgent.IsOnShelter
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