using UnityEngine;

namespace Enemy.Ai.AiContextInterfaces
{
    public interface IAiShelterRepositionAgent
    {
        public bool IsOnShelter { get; }
        public bool IsShelterPossible { get; }
        public Vector3? FarthestValidShelterPoint { get; }
    }
}