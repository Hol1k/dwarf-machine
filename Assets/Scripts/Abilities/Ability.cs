using Character;
using UnityEngine;

namespace Abilities
{
    public abstract class Ability : ScriptableObject
    {
        public abstract float Cooldown { get; }
        public abstract void Cast(CharacterAbilitiesController handler);
    }
}