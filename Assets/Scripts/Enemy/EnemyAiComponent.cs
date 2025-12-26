using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyAiComponent : MonoBehaviour
    {
        private EnemyFsm _fsm;

        public EnemyTypeId enemyType;

        [Inject]
        private void Init(EnemyFsmFactory fsmFactory, EnemyAiContext aiContext)
        {
            _fsm = fsmFactory.Create(enemyType, aiContext);
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}