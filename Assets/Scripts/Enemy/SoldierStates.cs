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
                else
                {
                    aiContext.MoveTo(fsmContext.PatrolPoint.Value);
                    if (!aiContext.IsAgentArrivedToDestination)
                    {
                        aiContext.MoveTo(fsmContext.PatrolPoint.Value);
                    }
                    else
                    {
                        fsmContext.PatrolPoint = null;
                        fsmContext.RequestedState = EnemyFsmStateId.Idle;
                    }
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
            void Update(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                if (!aiContext.IsSeeTarget && !aiContext.IsTargetEliminated)
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Alert;
                }
                else if (aiContext.IsShelterPossible && !aiContext.IsOnShelter
                         || !aiContext.CanAttackTarget)
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Reposition;
                }
                else if (!aiContext.IsTargetEliminated)
                {
                    aiContext.LookAt(aiContext.ClosestTarget.transform.position);
                    aiContext.AttackTarget(aiContext.ClosestTarget);
                }
                else
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Patrol;
                }
            }
            
            return new EnemyFsmState(null, Update, null);
        }

        public static EnemyFsmState GetAlertState()
        {
            void Enter(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                fsmContext.LookingTimer = 5f;
            }
            
            void Update(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                if (aiContext.IsSeeTarget)
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Combat;
                }
                else if (aiContext.LastSeePosition == null)
                {
                    fsmContext.RequestedState = EnemyFsmStateId.Patrol;
                }
                else
                {
                    aiContext.MoveTo(aiContext.LastSeePosition.Value);
                    if (!aiContext.IsAgentArrivedToDestination)
                    {
                        aiContext.MoveTo(aiContext.LastSeePosition.Value);
                    }
                    else if (fsmContext.LookingTimer >= 0f)
                    {
                        fsmContext.LookingTimer -= Time.deltaTime;
                    }
                    else
                    {
                        fsmContext.RequestedState = EnemyFsmStateId.Patrol;
                    }
                }
            }
            
            void Exit(EnemyAiContext aiContext, EnemyFsmContext fsmContext)
            {
                aiContext.StopMove();
            }
            
            return new EnemyFsmState(Enter, Update, Exit);
        }

        public static EnemyFsmState GetRepositionState()
        {
            return new EnemyFsmState(null, null, null);
        }
    }
}