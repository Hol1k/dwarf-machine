using UnityEngine;

namespace Enemy
{
    public class EnemyAiContext
    {
        public bool IsSeePlayer { get; private set; }
        public Vector3? LastSeePosition { get; set; }
    }
}