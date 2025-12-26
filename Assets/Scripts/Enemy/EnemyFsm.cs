using System.Collections.Generic;

namespace Enemy
{
    public class EnemyFsm
    {
        private readonly EnemyFsmContext _fsmContext = new();
        private readonly EnemyAiContext _aiContext;
        
        private readonly Dictionary<EnemyFsmStateId, EnemyFsmState> states = new();

        private EnemyFsmStateId _currentState;

        public EnemyFsm(
            EnemyFsmState idleState,
            EnemyFsmState patrolState,
            EnemyFsmState combatState,
            EnemyFsmState alertState,
            EnemyFsmState repositionState,
            EnemyAiContext aiContext)
        {
            states.Add(EnemyFsmStateId.Idle,  idleState);
            states.Add(EnemyFsmStateId.Patrol,  patrolState);
            states.Add(EnemyFsmStateId.Combat,  combatState);
            states.Add(EnemyFsmStateId.Alert,  alertState);
            states.Add(EnemyFsmStateId.Reposition,  repositionState);
            _aiContext = aiContext;
            
            _currentState = EnemyFsmStateId.Idle;
        }

        public void Update()
        {
            states[_currentState].Update(_aiContext, _fsmContext);

            if (_fsmContext.RequestedState.HasValue)
            {
                if (_fsmContext.RequestedState.Value == _currentState)
                {
                    _fsmContext.RequestedState = null;
                    return;
                }
                
                SwapState(_fsmContext.RequestedState.Value);
                _fsmContext.RequestedState = null;
            }
        }

        private void SwapState(EnemyFsmStateId state)
        {
            states[_currentState].Exit(_aiContext, _fsmContext);
            
            _currentState = state;
            
            states[_currentState].Enter(_aiContext, _fsmContext);
        }
    }
}