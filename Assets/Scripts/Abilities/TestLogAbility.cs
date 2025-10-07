using UnityEngine;

namespace Abilities
{
    [CreateAssetMenu(fileName = "NewTestLogAbility", menuName = "Abilities/TestLogAbility", order = 0)]
    public class TestLogAbility : Ability
    {
        public string logMessage = "TestLogAbility Cast";
        
        public override void Cast()
        {
            Debug.Log(logMessage);
        }
    }
}