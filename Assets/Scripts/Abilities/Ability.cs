using UnityEngine;

namespace Abilities
{
    public abstract class Ability : ScriptableObject
    {
        public abstract void Cast();
    }
}