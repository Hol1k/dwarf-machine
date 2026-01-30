using UnityEngine;

namespace Enemy.AiContextInterfaces
{
    public interface IAiShelterRepositionAgent
    {
        public bool IsOnShelter { get; }
        public bool IsShelterPossible { get; }
        public Vector3? FarthestValidShelterPoint { get; }
    }
}