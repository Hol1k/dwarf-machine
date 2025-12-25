using System.Collections.Generic;

namespace Enemy
{
    public class EnemyFsm
    {
        private readonly EnemyFsmContext ctx = new();
        
        private readonly Dictionary<EnemyFsmStateID, EnemyFsmState> states = new();

        private EnemyFsmStateID _currentState;

        public EnemyFsm(EnemyFsmState idleState, EnemyFsmState patrolState, EnemyFsmState combatState, EnemyFsmState alertState, EnemyFsmState repositionState)
        {
            states.Add(EnemyFsmStateID.Idle,  idleState);
            states.Add(EnemyFsmStateID.Patrol,  patrolState);
            states.Add(EnemyFsmStateID.Combat,  combatState);
            states.Add(EnemyFsmStateID.Alert,  alertState);
            states.Add(EnemyFsmStateID.Reposition,  repositionState);
            
            _currentState = EnemyFsmStateID.Idle;
        }

        public void Update()
        {
            states[_currentState].Update(ctx);

            if (ctx.RequestedState.HasValue)
            {
                if (ctx.RequestedState.Value == _currentState)
                {
                    ctx.RequestedState = null;
                    return;
                }
                
                SwapState(ctx.RequestedState.Value);
                ctx.RequestedState = null;
            }
        }

        private void SwapState(EnemyFsmStateID state)
        {
            states[_currentState].Exit(ctx);
            
            _currentState = state;
            
            states[_currentState].Enter(ctx);
        }
    }
}