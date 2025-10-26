using UnityEngine;

namespace Player
{
    [CreateAssetMenu(fileName = "NewEnemyStatsConfig", menuName = "Configs/EnemyStatsConfig", order = 0)]
    public class EnemyStatsConfig : ScriptableObject
    {
        public float maxHealth = 100f;
        public float currentHealth = 100f;
    }
}