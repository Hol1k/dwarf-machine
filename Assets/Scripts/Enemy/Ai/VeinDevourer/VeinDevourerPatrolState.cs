using System.Linq;
using Enemy.Ai.AiContextInterfaces;
using UnityEngine;

namespace Enemy.Ai.VeinDevourer
{
    public class VeinDevourerPatrolState : EnemyFsmState
    {
        private readonly IAiLootCollectionAgent _lootCollectionAgent;
        private readonly IAiMoveAgent _moveAgent;
        private readonly IAiTransformAgent _transformAgent;
        
        public VeinDevourerPatrolState(VeinDevourerAiContext aiContext)
        {
            _lootCollectionAgent = aiContext;
            _moveAgent = aiContext;
            _transformAgent = aiContext;
        }
        
        public override void Enter(EnemyFsmContext fsmContext)
        {
        }

        public override void Update(EnemyFsmContext fsmContext)
        {
            if (!_lootCollectionAgent.ClosestOreVeinTransform)
            {
                fsmContext.RequestedState = EnemyFsmStateId.Idle;
            }
            else if (Vector3.Distance(_transformAgent.SelfPosition, _lootCollectionAgent.ClosestOreVeinTransform.position) > 2f)
            {
                _moveAgent.MoveTo(_lootCollectionAgent.ClosestOreVeinTransform.position);
            }
            else
            {
                _moveAgent.StopMove();
                fsmContext.RequestedState = EnemyFsmStateId.Eating;
            }
        }

        public override void Exit(EnemyFsmContext fsmContext)
        {
        }
    }
}