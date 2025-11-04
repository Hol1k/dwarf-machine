using UnityEngine;

namespace Modifiers
{
    public class MovementModifier : Modifier
    {
        private readonly Vector2 _value;

        public MovementModifier(float durationTime, ModifierType type, Vector2 value) : base(durationTime, type)
        {
            _value = value;
        }

        public void Apply(ref Vector2 movement)
        {
            switch (Type)
            {
                case ModifierType.Addition:
                    movement += _value;
                    break;
                case ModifierType.Multiplication:
                    movement *= _value;
                    break;
            }
        }
    }
}