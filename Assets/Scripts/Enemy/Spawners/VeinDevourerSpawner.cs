using System.Linq;
using UnityEngine;
using Zenject;

namespace Enemy.Spawners
{
    public partial class VeinDevourerSpawner : MonoBehaviour
    {
        [SerializeField] private Transform spawnPointsCollectionParent;

        private Transform[] SpawnPointsCollection => spawnPointsCollectionParent.GetComponentsInChildren<Transform>()
            .Where(point => point != spawnPointsCollectionParent).ToArray();
        
        [Inject] private VeinDevourerFactory veinDevourerFactory;

        private void OnDrawGizmosSelected()
        {
            foreach (var spawnPoint in SpawnPointsCollection)
            {
                Gizmos.DrawSphere(spawnPoint.position, 0.5f);
            }
        }

        public void SpawnAll()
        {
            foreach (var spawnPoint in SpawnPointsCollection)
            {
                var enemy = veinDevourerFactory.Create();
                enemy.transform.position = spawnPoint.position;
            }
        }
    }
}