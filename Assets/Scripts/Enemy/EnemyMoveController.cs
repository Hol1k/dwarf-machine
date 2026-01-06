using System.Collections;
using Level;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMoveController : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private NavMeshSurface _surface;
        
        [Inject]
        private void Init(NavMeshAgent agent, NavMeshSurfaceController surfaceController)
        {
            _agent = agent;
            _agent.enabled = false;
            
            _surface = surfaceController.Surface;
        }

        private void Awake()
        {
            StartCoroutine(EnableAgent());
        }

        public void MoveTo(Vector3 position) => _agent.SetDestination(position);

        public bool IsAgentArrivedToDestination()
        {
            if (!_agent.enabled)
                return false;

            if (_agent.pathPending)
                return false;

            if (!_agent.hasPath)
                return false;

            if (_agent.pathStatus == NavMeshPathStatus.PathInvalid)
                return true;

            var agentPos = new Vector2(_agent.nextPosition.x, _agent.nextPosition.z);
            var endPos = new Vector2(_agent.pathEndPosition.x, _agent.pathEndPosition.z);
            var distanceFromEndOfPath = Vector2.Distance(agentPos, endPos);
            
            return distanceFromEndOfPath < 0.05f;
        }

        private IEnumerator EnableAgent()
        {
            while (!_surface.navMeshData)
            {
                yield return null;
            }
            _agent.enabled = true;
        }
    }
}