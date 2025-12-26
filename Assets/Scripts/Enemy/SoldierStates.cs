using UnityEngine;

namespace Enemy
{
    public static class SoldierStates
    {
        public static EnemyFsmState GetIdleState()
        {
            void Enter(EnemyFsmContext ctx)
            {
                Debug.Log($"Entered to Idle state for {ctx.IdleTimer} seconds");
            }
            
            void Update(EnemyFsmContext ctx)
            {
                if (ctx.IdleTimer <= 0)
                {
                    ctx.RequestedState = EnemyFsmStateId.Patrol;
                }
                
                ctx.IdleTimer -= Time.deltaTime;
            }

            void Exit(EnemyFsmContext ctx)
            {
                ctx.IdleTimer = 0f;
            }

            return new EnemyFsmState(Enter, Update, Exit);
        }
        
        public static EnemyFsmState GetPatrolState()
        {
            void Enter(EnemyFsmContext ctx)
            {
                Debug.Log("Entered to Patrol state");
            }
            
            void Update(EnemyFsmContext ctx)
            {
                var idleTime = Random.Range(0f, 3f);
                ctx.IdleTimer = idleTime;
                ctx.RequestedState = EnemyFsmStateId.Idle;
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