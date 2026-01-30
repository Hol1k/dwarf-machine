using Character;
using UnityEngine;

namespace Enemy.AiContextInterfaces
{
    public interface IAiCombatAgent
    {
        public bool CanAttackTarget { get; }
        public bool CanAttackTargetFrom(Vector3 position);
        public bool IsTargetEliminated { get; }
        public void AttackTarget(CharacterStatsComponent target);
    }
}