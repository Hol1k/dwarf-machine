using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "NewCharacterStatsConfig", menuName = "Configs/CharacterStatsConfig", order = 0)]
    public class CharacterStatsConfig : ScriptableObject
    {
        public float maxHealth = 100f;
        public float currentHealth = 100f;
        [Tooltip("No using")] public float armourX = 0f;
        [Space]
        public float moveSpeed = 7f;
        public float dashRange = 4f;
        public float dashDuration = 0.5f;
        public float dashCooldown = 1f;
        public float jumpHeight = 2f;
    }
}