using Character;
using UnityEngine;

namespace Enemy.AiContextInterfaces
{
    public interface IAiLookAgent
    {
        public bool IsSeeTarget { get; }
        public Vector3? LastSeePosition { get; }
        public float LookRange { get; }
        public void ForgetLastSeePosition();
        public CharacterStatsComponent ClosestTarget { get; }
    }
}