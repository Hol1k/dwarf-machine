using Abilities;
using UnityEngine;

namespace Character
{
    public class CharacterAbilitiesController : MonoBehaviour
    {
        public Ability Ability1;
        public Ability Ability2;
        public Ability Ability3;
        public Ability Ability4;

        public void OnAbility1()
        {
            if (!Ability1)
            {
                Debug.LogError("Ability1 is not implemented");
                return;
            }
            
            Ability1.Cast();
        }

        public void OnAbility2()
        {
            if (Ability2 == null)
            {
                Debug.LogError("Ability2 is not implemented");
                return;
            }

            Ability2.Cast();
        }

        public void OnAbility3()
        {
            if (Ability3 == null)
            {
                Debug.LogError("Ability3 is not implemented");
                return;
            }

            Ability3.Cast();
        }

        public void OnAbility4()
        {
            if (Ability4 == null)
            {
                Debug.LogError("Ability4 is not implemented");
                return;
            }

            Ability4.Cast();
        }
    }
}