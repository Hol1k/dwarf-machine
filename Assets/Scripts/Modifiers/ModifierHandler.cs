using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Modifiers
{
    public class ModifierHandler : MonoBehaviour
    {
        private readonly List<Modifier> _modifiers = new List<Modifier>();
        
        private void Update()
        {
            foreach (Modifier modifier in _modifiers.ToList())
            {
                modifier.TimeLeft -= Time.deltaTime;
                if (modifier.TimeLeft <= 0f)
                    _modifiers.Remove(modifier);
            }
        }

        public Vector2 ModifyMovement(Vector2 movement)
        {
            var movementModifiers = _modifiers
                .OfType<MovementModifier>();

            var addingModifiers = movementModifiers
                .Where(m => m.Type == ModifierType.Addition)
                .ToList();
            
            var multiplicationModifiers = movementModifiers
                .Where(m => m.Type == ModifierType.Multiplication)
                .ToList();

            foreach (var modifier in addingModifiers)
                modifier.Apply(ref movement);
            foreach (var modifier in multiplicationModifiers)
                modifier.Apply(ref movement);
            
            return movement;
        }

        public void AddModifier(Modifier modifier)
        {
            _modifiers.Add(modifier);
        }
    }
}