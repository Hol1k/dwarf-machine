using System.Collections;
using Level;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using Zenject;

namespace Enemy.Ai
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyMoveController : MonoBehaviour
    {
        private NavMeshAgent _agent;
        private NavMeshSurface _surface;
        
        private MoveControllerAction? _lastActionRequested = null;
        private enum MoveControllerAction
        {
            MoveTo,
            StopMove
        }
        private Vector3 _requestedPos;
        
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

        public void MoveTo(Vector3 position)
        {
            if (!_agent.enabled)
            {
                _lastActionRequested = MoveControllerAction.MoveTo;
                _requestedPos = position;
                return;
            }
            
            _agent.isStopped = false;
            _agent.SetDestination(position);
        }

        public void StopMove()
        {
            if (!_agent.enabled)
            {
                _lastActionRequested = MoveControllerAction.StopMove;
                return;
            }

            _agent.isStopped = true;
        }

        public void LookAt(Vector3 target)
        {
            target.y = transform.position.y;
            
            transform.LookAt(target);
        }

        private void Update()
        {
            RunLastRequestedAction();
        }

        private void RunLastRequestedAction()
        {
            if (_lastActionRequested == null) return;
            
            switch (_lastActionRequested.Value)
            {
                case MoveControllerAction.MoveTo:
                    MoveTo(_requestedPos);
                    break;
                case MoveControllerAction.StopMove:
                    StopMove();
                    break;
            }
            _lastActionRequested = null;
        }

        public bool IsAgentArrivedToDestination()
        {
            if (!_agent.enabled)
                return false;

            if (_agent.pathPending)
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