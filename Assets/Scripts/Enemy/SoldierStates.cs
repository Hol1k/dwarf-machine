using UnityEngine;

namespace Enemy
{
    public static class SoldierStates
    {
        public static EnemyFsmState GetIdleState()
        {
            void Enter(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                Debug.Log($"Entered to Idle state for {fsmContext.IdleTimer} seconds");
            }
            
            void Update(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                if (fsmContext.IdleTimer <= 0)
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Patrol;
                }
                
                fsmContext.IdleTimer -= Time.deltaTime;
            }

            void Exit(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                fsmContext.IdleTimer = 0f;
            }

            return new EnemyFsmState(Enter, Update, Exit);
        }
        
        public static EnemyFsmState GetPatrolState()
        {
            void Enter(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                Debug.Log("Entered to Patrol state");
            }
            
            void Update(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                var idleTime = Random.Range(0f, 3f);
                fsmContext.IdleTimer = idleTime;
                fsmContext.RequestedState = EnemyFsmStateId.Idle;
            }

            return new EnemyFsmState(Enter, Update, null);
        }
        
        public static EnemyFsmState GetCombatState()
        {
            return new EnemyFsmState(null, null, null);
        }
        
        public static EnemyFsmState GetAlertState()
        {
            return new EnemyFsmState(null, null, null);
        }
        
        public static EnemyFsmState GetRepositionState()
        {
            return new EnemyFsmState(null, null, null);
        }
    }
}