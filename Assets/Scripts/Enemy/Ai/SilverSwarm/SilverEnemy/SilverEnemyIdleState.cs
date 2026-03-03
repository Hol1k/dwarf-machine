using Enemy.Ai.AiContextInterfaces;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy.Ai.SilverSwarm.SilverEnemy
{
    public class SilverEnemyIdleState : EnemyFsmState
    {
        private readonly IAiSwarmDataAgent _swarmDataAgent;
        
        public SilverEnemyIdleState(SilverSwarmAiContext aiContext)
        {
            _swarmDataAgent = aiContext;
        }

        public override void Enter(EnemyFsmContext fsmContext)
        {
            fsmContext.IdleTimer = Random.Range(0.5f, 1.5f);
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_swarmDataAgent.AttackFlag)
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
            Debug.Log("Exit SilverSwarm");
            fsmContext.IdleTimer = 0f;
        }
    }
}