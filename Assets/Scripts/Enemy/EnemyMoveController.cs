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

            var agentPos = _agent.nextPosition;
            var agentY = _agent.nextPosition.y - _agent.height / 2;
            agentPos.y = agentY;
            var distanceFromEndOfPath = Vector3.Distance(agentPos, _agent.pathEndPosition);
            
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