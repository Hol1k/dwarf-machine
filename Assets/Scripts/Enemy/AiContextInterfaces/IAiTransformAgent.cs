using UnityEngine;

namespace Enemy.AiContextInterfaces
{
    public interface IAiTransformAgent
    {
        public Vector3 EnemyPosition { get; }
    }
}