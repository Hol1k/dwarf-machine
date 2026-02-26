using UnityEngine;

namespace Enemy.Ai
{
    public abstract class EnemyPatrolComponent : MonoBehaviour
    {
        public abstract Vector3 GetNextPoint(Vector3 startPos);
    }
}