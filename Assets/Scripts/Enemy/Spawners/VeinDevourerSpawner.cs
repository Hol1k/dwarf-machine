using UnityEngine;
using Zenject;

namespace Enemy.Spawners
{
    public partial class VeinDevourerSpawner : MonoBehaviour, IEnemySpawner
    {
        [Inject] private VeinDevourerFactory veinDevourerFactory;

        public void SpawnAll()
        {
            var enemy = veinDevourerFactory.Create();
            enemy.transform.position = transform.position;
        }
    }
}