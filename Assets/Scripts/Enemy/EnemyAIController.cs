using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyAIController : MonoBehaviour
    {
        private EnemyFsm _fsm;

        public EnemyTypeID enemyType;

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