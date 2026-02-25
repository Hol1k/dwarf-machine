using Enemy.Ai.AiContextInterfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Ai.SilverSwarm
{
    public class SilverSwarmIdleState : EnemyFsmState
    {
        private readonly IAiLookAgent _lookAgent;
        
        public SilverSwarmIdleState(SilverSwarmAiContext aiContext)
        {
            _lookAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
            _lookAgent.ForgetLastSeePosition();
            fsmContext.IdleTimer = Random.Range(3f, 5f);
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_lookAgent.IsSeeTarget)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Combat;
            }
            else if (fsmContext.IdleTimer > 0f)
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