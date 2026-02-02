using Enemy.Ai.AiContextInterfaces;
using Enemy.Humanoids;

namespace Enemy.Ai.Humanoids.MeleeAborigine
{
    public class MeleeAborigineRepositionState : EnemyFsmState
    {
        private readonly IAiMoveAgent _moveAgent;
        private readonly IAiCombatAgent _combatAgent;
        private readonly IAiLookAgent _lookAgent;
        
        public MeleeAborigineRepositionState(HumanoidAiContext aiContext)
        {
            _moveAgent = aiContext;
            _combatAgent = aiContext;
            _lookAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_combatAgent.IsTargetEliminated)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Patrol;
            }
            else if (fsmContext.RepositionPoint == null ||
                     !_combatAgent.CanAttackTargetFrom(fsmContext.RepositionPoint.Value))
            {
                fsmContext.RepositionPoint = _lookAgent.ClosestTarget.transform.position;
            }
            else
            {
                _moveAgent.MoveTo(fsmContext.RepositionPoint.Value);
                if (!_combatAgent.CanAttackTarget)
                {
                    _moveAgent.MoveTo(fsmContext.RepositionPoint.Value);
                }
                else
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Combat;
                }
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
            fsmContext.RepositionPoint = null;
            _moveAgent.StopMove();
        }
    }
}