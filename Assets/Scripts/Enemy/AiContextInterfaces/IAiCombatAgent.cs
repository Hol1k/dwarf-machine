using Character;

namespace Enemy.AiContextInterfaces
{
    public interface IAiCombatAgent
    {
        public bool CanAttackTarget { get; }
        public bool IsTargetEliminated { get; }
        public void AttackTarget(CharacterStatsComponent target);
    }
}