using Abilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public class CharacterAbilitiesController : MonoBehaviour
    {
        public Ability ability1;
        public Ability ability2;

        public void OnAbility1()
        {
            if (!ability1)
            {
                Debug.LogError("Ability1 is not implemented");
                return;
            }
            
            ability1.Cast();
        }

        public void OnAbility2()
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