using UnityEngine;

namespace Enemy
{
    public static class SoldierStates
    {
        public static EnemyFsmState IdleState()
        {
            void Enter(EnemyFsmContext ctx)
            {
                Debug.Log("Entered to Idle state");
            }
            
            void Update(EnemyFsmContext ctx)
            {
                if (ctx.IdleTimer <= 0)
                {
                    ctx.RequestedState = EnemyFsmStateID.Patrol;
                }
                
                ctx.IdleTimer -= Time.deltaTime;
            }

            void Exit(EnemyFsmContext ctx)
            {
                ctx.IdleTimer = 0f;
            }

            return new EnemyFsmState(Enter, Update, Exit);
        }
        
        public static EnemyFsmState PatrolState()
        {
            void Enter(EnemyFsmContext ctx)
            {
                Debug.Log("Entered to Patrol state");
            }
            
            void Update(EnemyFsmContext ctx)
            {
                var idleTime = Random.Range(0f, 3f);
                Debug.Log($"Requested Idle State with {idleTime} seconds duration");
                ctx.IdleTimer = idleTime;
                ctx.RequestedState = EnemyFsmStateID.Idle;
            }

            return new EnemyFsmState(Enter, Update, null);
        }
        
        public static EnemyFsmState CombatState()
        {
            return new EnemyFsmState(null, null, null);
        }
        
        public static EnemyFsmState AlertState()
        {
            return new EnemyFsmState(null, null, null);
        }
        
        public static EnemyFsmState RepositionState()
        {
            return new EnemyFsmState(null, null, null);
        }
    }
}