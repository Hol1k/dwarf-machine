using UnityEngine;
using Zenject;

namespace Enemy.Ai.Humanoids.Soldier
{
    public class SoldierAiComponent : EnemyAiComponent
    {
        private EnemyFsm _fsm;

        [Inject]
        private void Init(SoldierFsmFactory fsmFactory, EnemyAiContext aiContext)
        {
            _fsm = fsmFactory.Create(aiContext);
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}