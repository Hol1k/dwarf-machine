using UnityEngine;
using Zenject;

namespace Enemy.Ai.Humanoids.MeleeAborigine
{
    public class MeleeAborigineAiComponent : MonoBehaviour
    {
        private EnemyFsm _fsm;

        [Inject]
        private void Init(MeleeAborigineFsmFactory fsmFactory, EnemyAiContext aiContext)
        {
            _fsm = fsmFactory.Create(aiContext);
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}