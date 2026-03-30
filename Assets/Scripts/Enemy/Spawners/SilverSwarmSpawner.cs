using UnityEngine;
using Zenject;

namespace Enemy.Spawners
{
    public partial class SilverSwarmSpawner : MonoBehaviour, IEnemySpawner
    {
        [Inject] private SilverSwarmFactory silverSwarmFactoryFactory;

        public void SpawnAll()
        {
            var enemy = silverSwarmFactoryFactory.Create();
            enemy.transform.position = transform.position;
        }
    }
}