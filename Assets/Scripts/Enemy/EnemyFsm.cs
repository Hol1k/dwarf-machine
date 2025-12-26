using System.Collections.Generic;

namespace Enemy
{
    public class EnemyFsm
    {
        private readonly EnemyFsmContext ctx = new();
        
        private readonly Dictionary<EnemyFsmStateId, EnemyFsmState> states = new();

        private EnemyFsmStateId _currentState;

        public EnemyFsm(EnemyFsmState idleState, EnemyFsmState patrolState, EnemyFsmState combatState, EnemyFsmState alertState, EnemyFsmState repositionState)
        {
            states.Add(EnemyFsmStateId.Idle,  idleState);
            states.Add(EnemyFsmStateId.Patrol,  patrolState);
            states.Add(EnemyFsmStateId.Combat,  combatState);
            states.Add(EnemyFsmStateId.Alert,  alertState);
            states.Add(EnemyFsmStateId.Reposition,  repositionState);
            
            _currentState = EnemyFsmStateId.Idle;
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

        private void SwapState(EnemyFsmStateId state)
        {
            states[_currentState].Exit(ctx);
            
            _currentState = state;
            
            states[_currentState].Enter(ctx);
        }
    }
}