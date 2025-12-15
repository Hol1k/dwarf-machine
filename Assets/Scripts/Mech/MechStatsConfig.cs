using UnityEngine;

namespace Mech
{
    [CreateAssetMenu(fileName = "NewMechStatsConfig", menuName = "Configs/MechStatsConfig", order = 0)]
    public class MechStatsConfig : ScriptableObject
    {
        public float maxHealth = 1000f;
        public float currentHealth = 1000f;
        [Space]
        public float moveSpeed = 5f;
    }
}