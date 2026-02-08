using UnityEngine;
using Zenject;

namespace Enemy.Ai.Humanoids.MeleeAborigine
{
    public class MeleeAborigineAiComponent : EnemyAiComponent
    {
        [Inject]
        private void Init(MeleeAborigineFsmFactory fsmFactory, EnemyAiContext aiContext)
        {
            Fsm = fsmFactory.Create(aiContext);
        }

        private void Update()
        {
            Fsm.Update();
        }
    }
}