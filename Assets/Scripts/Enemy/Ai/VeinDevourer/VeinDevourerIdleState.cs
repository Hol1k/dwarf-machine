using System.Linq;
using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai.VeinDevourer
{
    public class VeinDevourerIdleState : EnemyFsmState
    {
        private readonly IAiLookAgent _lookAgent;
        
        public VeinDevourerIdleState(VeinDevourerAiContext aiContext)
        {
            _lookAgent = aiContext;
        }
        
        public override void Enter(EnemyFsmContext fsmContext)
        {
            fsmContext.IdleTimer = Random.Range(60f, 90f);
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (_lookAgent.IsSeeTarget &&
                     (_lookAgent.ClosestTargetInventoryValue?.Values.Sum() ?? 0f) > 100f)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Reposition;
            }
            else if (fsmContext.IdleTimer >= 0f)
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