using Character;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public abstract class EnemyCombatComponent : MonoBehaviour
    {
        protected EnemyAiContext AiContext;

        public abstract bool CanAttackTarget { get; }

        public bool IsTargetEliminated
        {
            get
            {
                if (!_isTargetEliminated) return false;
                
                _isTargetEliminated = false;
                return true;
            }

            protected set => _isTargetEliminated = value;
        }
        private bool _isTargetEliminated;

        [Inject]
        protected void Init(EnemyAiContext aiContext)
        {
            AiContext = aiContext;
        }

        public abstract void AttackTarget(CharacterStatsComponent target);
    }
}