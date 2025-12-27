using UnityEngine;

namespace Enemy
{
    public static class SoldierStates
    {
        public static EnemyFsmState GetIdleState()
        {
            void Enter(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                aiContext.LastSeePosition = null;
            }
            
            void Update(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                if (aiContext.IsSeePlayer)
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Combat;
                }
                else if (fsmContext.IdleTimer > 0)
                {
                    fsmContext.IdleTimer -= Time.deltaTime;
                }
                else
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Patrol;
                }
            }

            void Exit(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                fsmContext.IdleTimer = 0f;
            }

            return new EnemyFsmState(Enter, Update, Exit);
        }
        
        public static EnemyFsmState GetPatrolState()
        {
            void Update(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                var idleTime = Random.Range(0f, 3f);
                fsmContext.IdleTimer = idleTime;
                fsmContext.RequestedState = EnemyFsmStateId.Idle;
            }

            return new EnemyFsmState(null, Update, null);
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