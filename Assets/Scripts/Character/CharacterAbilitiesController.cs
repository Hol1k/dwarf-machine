using Abilities;
using UnityEngine;

namespace Character
{
    public class CharacterAbilitiesController : MonoBehaviour
    {
        public Ability ability1;
        public Ability ability2;
        
        private float _ability1CastTime = 0f;
        private float _ability2CastTime = 0f;

        public void CastAbility1Request()
        {
            if (!ability1)
            {
                Debug.LogError("Ability1 is not implemented");
                return;
            }

            if (!(Time.time - _ability1CastTime > ability1.Cooldown)) 
                return;
            
            ability1.Cast(this);
            _ability1CastTime = Time.time;
        }

        public void CastAbility2Request()
        {
            if (ability2 == null)
            {
                Debug.LogError("Ability2 is not implemented");
                return;
            }

            if (!(Time.time - _ability2CastTime > ability2.Cooldown)) 
                return;

            ability2.Cast(this);
            _ability2CastTime = Time.time;
        }
    }
}