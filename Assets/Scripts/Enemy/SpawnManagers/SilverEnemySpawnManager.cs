using UnityEngine;
using Zenject;

namespace Enemy.SpawnManagers
{
    public class SilverEnemySpawnManager : MonoBehaviour
    {
        [SerializeField] private Transform silversCollection;
        
        [Space]
        [SerializeField] private int silversCount;
        
        [Inject] private SilverEnemyFactory silverEnemyFactoryFactory;

        private void Start()
        {
            SpawnAll();
        }

        private void SpawnAll()
        {
            for (int i = 0; i < silversCount; i++)
            {
                var enemy = silverEnemyFactoryFactory.Create(silversCollection);
                enemy.transform.position = transform.position;
            }
        }
    }
}