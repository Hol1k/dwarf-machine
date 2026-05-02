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
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (fsmContext.IdleTimer > 0f)
            {
                fsmContext.IdleTimer -= Time.deltaTime;
            }
            else if (_lookAgent.IsSeeTarget &&
                     (_lookAgent.ClosestTargetInventoryValue?.Values.Sum() ?? 0f) > 100f)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Reposition;
            }
            else
            {
                fsmContext.IdleTimer = 1f;
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
        }
    }
}