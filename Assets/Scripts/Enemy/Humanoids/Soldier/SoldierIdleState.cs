using Enemy.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Humanoids.Soldier
{
    public class SoldierIdleState : EnemyFsmState
    {
        private readonly IAiLookAgent _lookAgent;

        public SoldierIdleState(HumanoidAiContext aiContext)
        {
            _lookAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
            _lookAgent.ForgetLastSeePosition();
            fsmContext.IdleTimer = Random.Range(1f, 3f);
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_lookAgent.IsSeeTarget)
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

        public override void Exit(EnemyFsmContext fsmContext)
        {
            fsmContext.IdleTimer = 0f;
        }
    }
}