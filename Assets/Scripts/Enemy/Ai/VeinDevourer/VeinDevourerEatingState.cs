using System.Linq;
using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai.VeinDevourer
{
    public class VeinDevourerEatingState : EnemyFsmState
    {
        private readonly IAiLootCollectionAgent _lootCollectionAgent;
        
        public VeinDevourerEatingState(VeinDevourerAiContext aiContext)
        {
            _lootCollectionAgent = aiContext;
        }
        
        public override void Enter(EnemyFsmContext fsmContext)
        {
            fsmContext.IdleTimer = Random.Range(10f, 15f);
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (fsmContext.IdleTimer >= 0f)
            {
                fsmContext.IdleTimer -= Time.deltaTime;
            }
            else
            {
                fsmContext.RequestedState = EnemyFsmStateId.Idle;
                _lootCollectionAgent.DestroyClosestOreVein();
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
            fsmContext.IdleTimer = 0f;
        }
    }
}