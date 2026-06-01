using Character;
using Entities;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "NewHealAbility", menuName = "Abilities/HealAbility", order = 0)]
    public class HealAbility : Ability
    {
        public int healAmount;
        [SerializeField] [Min(0)] private float cooldown;
        public override float Cooldown => cooldown;

        public override void Cast(CharacterAbilitiesController handler)
        {
            if (handler.TryGetComponent(out IDamageable damageable))
                damageable.Health += healAmount;
        }
    }
}