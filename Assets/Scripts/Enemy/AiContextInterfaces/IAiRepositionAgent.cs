using UnityEngine;

namespace Enemy.AiContextInterfaces
{
    public interface IAiRepositionAgent
    {
        public bool IsOnShelter { get; }
        public bool IsShelterPossible { get; }
        public Vector3? FarthestValidShelterPoint { get; }
    }
}