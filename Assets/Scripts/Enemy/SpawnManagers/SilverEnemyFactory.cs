using Enemy.Ai;
using UnityEngine;
using Zenject;

namespace Enemy.SpawnManagers
{
    public class SilverEnemyFactory : PlaceholderFactory<EnemyAiComponent>
    {
        public EnemyAiComponent Create(Transform parent)
        {
            var enemy = base.Create();
            enemy.transform.SetParent(parent);
            return enemy;
        }
    }
}