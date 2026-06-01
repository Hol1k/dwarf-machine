using Character;
using Modifiers;
using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "NewSpeedBoostAbility", menuName = "Abilities/SpeedBoostAbility", order = 0)]
    public class SpeedBoostAbility : Ability
    {
        public float multiplicationSpeedValue;
        public float duration;
        [SerializeField] [Min(0)] private float cooldown;
        public override float Cooldown => cooldown;

        public override void Cast(CharacterAbilitiesController handler)
        {
            if (handler.TryGetComponent(out ModifierHandler modifierHandler))
                modifierHandler.AddModifier(
                    new MovementModifier(duration, ModifierType.Multiplication, new Vector2(multiplicationSpeedValue, multiplicationSpeedValue)));
        }
    }
}