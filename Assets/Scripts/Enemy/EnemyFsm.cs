using System;
using System.Collections.Generic;

namespace Enemy
{
    public class EnemyFsm
    {
        protected readonly EnemyFsmContext FsmContext = new();
        
        protected readonly Dictionary<EnemyFsmStateId, EnemyFsmState> States = new();

        protected EnemyFsmStateId CurrentState;
        
        public void Update()
        {
            States[CurrentState].Update(FsmContext);

            if (FsmContext.RequestedState.HasValue)
            {
                if (!States.ContainsKey(FsmContext.RequestedState.Value))
                {
                    throw new ArgumentException($"{ToString()} has no {FsmContext.RequestedState.Value} state");
                }
                
                if (FsmContext.RequestedState.Value == CurrentState)
                {
                    FsmContext.RequestedState = null;
                    return;
                }
                
                SwapState(FsmContext.RequestedState.Value);
                FsmContext.RequestedState = null;
            }
        }

        private void SwapState(EnemyFsmStateId state)
        {
            States[CurrentState].Exit(FsmContext);
            
            CurrentState = state;
            
            States[CurrentState].Enter(FsmContext);
        }
    }
}