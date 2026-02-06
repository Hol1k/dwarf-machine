using UnityEngine;
using Zenject;

namespace Enemy.Ai.Humanoids.RangedAborigine
{
    public class RangedAborigineAiComponent : MonoBehaviour
    {
        private EnemyFsm _fsm;

        [Inject]
        private void Init(RangedAborigineFsmFactory fsmFactory, EnemyAiContext aiContext)
        {
            _fsm = fsmFactory.Create(aiContext);
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}