using UnityEngine;

namespace Enemy
{
    public static class MeleeAborigineStates
    {
        public static EnemyFsmState GetIdleState()
        {
            void Enter(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                aiContext.ForgetLastSeePosition();
                fsmContext.IdleTimer = Random.Range(1f, 3f);
            }
            
            void Update(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                if (aiContext.IsSeeTarget)
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
            return new EnemyFsmState(null, null, null);
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