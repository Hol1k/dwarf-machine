using Abilities;
using UnityEngine;

namespace Character
{
    public class CharacterAbilitiesController : MonoBehaviour
    {
        public Ability ability1;
        public Ability ability2;

        public void CastAbility1Request()
        {
            if (!ability1)
            {
                Debug.LogError("Ability1 is not implemented");
                return;
            }
            
            ability1.Cast();
        }

        public void CastAbility2Request()
        {
            if (ability2 == null)
            {
                Debug.LogError("Ability2 is not implemented");
                return;
            }

            ability2.Cast();
        }
    }
}