namespace Modifiers
{
    public abstract class Modifier
    {
        public float TimeLeft;
        public readonly ModifierType Type;

        protected Modifier(float durationTime, ModifierType type)
        {
            TimeLeft = durationTime;
            Type = type;
        }
    }
}