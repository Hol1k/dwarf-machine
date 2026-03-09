using UnityEngine;

namespace Enemy.Ai.SilverSwarm
{
    public class SwarmController : MonoBehaviour
    {
        [SerializeField] [Min(0.0000001f)] private float swarmRadius;
        
        public bool IsPointInsideSwarm(Vector3 point)
        {
            var selfPos = new Vector2(transform.position.x, transform.position.z);
            var pointPos = new Vector2(point.x, point.z);
            return Vector2.Distance(selfPos, pointPos) <= swarmRadius;
        }

        public Vector3 GetPointInsideSwarm()
        {
            var selfPos = new Vector2(transform.position.x, transform.position.z);
            var randomPos = selfPos + Random.insideUnitCircle * swarmRadius;
            return new Vector3(randomPos.x, transform.position.y, randomPos.y);
        }
    }
}