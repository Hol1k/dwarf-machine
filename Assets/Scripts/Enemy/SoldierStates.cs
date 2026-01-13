using UnityEngine;

namespace Enemy
{
    public static class SoldierStates
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
            void Enter(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                aiContext.ForgetLastSeePosition();
            }
            
            void Update(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                if (aiContext.IsSeeTarget)
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Combat;
                }
                else if (fsmContext.PatrolPoint == null)
                {
                    fsmContext.PatrolPoint = aiContext.NextPatrolPoint;
                }
                else if (!aiContext.IsAgentArrivedToDestination())
                {
                    MoveTo(fsmContext.PatrolPoint.Value, aiContext);
                }
                else
                {
                    fsmContext.PatrolPoint = null;
                    fsmContext.RequestedState = EnemyFsmStateId.Idle;
                }
            }

            void Exit(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                aiContext.StopMove();
            }

            return new EnemyFsmState(Enter, Update, Exit);
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

        private static void MoveTo(Vector3 movePosition, EnemyAiContext aiContext)
        {
            aiContext.MoveTo(movePosition);
        }
    }
}