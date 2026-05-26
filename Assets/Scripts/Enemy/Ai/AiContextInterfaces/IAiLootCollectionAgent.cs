using UnityEngine;

namespace Enemy.Ai.AiContextInterfaces
{
    public interface IAiLootCollectionAgent
    {
        public Transform ClosestOreVeinTransform { get; }
        void DestroyClosestOreVein();
        public Transform ClosestWoodTransform { get; }
    }
}