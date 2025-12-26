using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyAiComponent : MonoBehaviour
    {
        private EnemyFsm _fsm;

        public EnemyTypeId enemyType;

        [Inject]
        private void Init(EnemyFsmFactory fsmFactory)
        {
            _fsm = fsmFactory.Create(enemyType);
        }

        private void Update()
        {
            _fsm.Update();
        }
    }
}