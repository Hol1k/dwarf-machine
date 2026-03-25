using UnityEngine;

namespace Enemy.Ai
{
    public readonly struct EnemySpawnPointsCollection
    {
        private readonly Transform[] _spawnPoints;
        
        public EnemySpawnPointsCollection(Transform[] spawnPoints)
        {
            _spawnPoints = spawnPoints;
        }
        public int Length => _spawnPoints.Length;
        public Vector3 this[int index] => _spawnPoints[index].position;
    }
}