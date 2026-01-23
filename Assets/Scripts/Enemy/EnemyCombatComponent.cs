using Character;
using Enemy.AiContextInterfaces;
using UnityEngine;
using Zenject;

namespace Enemy
{
    public abstract class EnemyCombatComponent : MonoBehaviour
    {
        protected IAiLookAgent LookAgent;

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
        protected void Init(IAiLookAgent lookAgent)
        {
            LookAgent = lookAgent;
        }

        public abstract void AttackTarget(CharacterStatsComponent target);
    }
}