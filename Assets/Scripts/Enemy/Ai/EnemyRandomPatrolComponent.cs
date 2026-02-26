using UnityEngine;

namespace Enemy.Ai
{
    public class EnemyRandomPatrolComponent : EnemyPatrolComponent
    {
        [SerializeField] private float nextPointRadius = 4;
        
        public override Vector3 GetNextPoint(Vector3 startPos)
        {
            var offset = new Vector2(Random.Range(-1f,1f), Random.Range(-1f,1f)).normalized;
            offset *= Random.Range(0f, nextPointRadius);
            
            return startPos + new Vector3(offset.x, 0f, offset.y);
        }
    }
}