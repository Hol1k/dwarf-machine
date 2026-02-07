using UnityEngine;

namespace Enemy.Ai.AiContextInterfaces
{
    public interface IAiTransformAgent
    {
        public Vector3 SelfPosition { get; }
    }
}