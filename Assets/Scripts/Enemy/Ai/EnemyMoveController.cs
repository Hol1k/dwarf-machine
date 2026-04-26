using System;
using System.Collections;
using System.Linq;
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
        
        private MoveControllerAction? _lastActionRequested = null;
        private enum MoveControllerAction
        {
            MoveTo,
            StopMove
        }
        private Vector3 _requestedPos;
        
        [Inject]
        private void Init(NavMeshAgent agent)
        {
            _agent = agent;
            _agent.enabled = false;
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
            
            var destinationVector2 = new Vector2(_agent.destination.x, _agent.destination.z);
            var positionVector2 = new Vector2(position.x, position.z);
            if (Vector2.Distance(destinationVector2, positionVector2) < 0.05f)
                return;
            
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
            if (!_agent.enabled || _agent.pathPending)
                return false;
            
            if (_agent.pathStatus is NavMeshPathStatus.PathInvalid or NavMeshPathStatus.PathPartial)
                return true;

            var agentPos = new Vector2(_agent.nextPosition.x, _agent.nextPosition.z);
            var endPos = new Vector2(_agent.pathEndPosition.x, _agent.pathEndPosition.z);
            var distanceFromEndOfPath = Vector2.Distance(agentPos, endPos);
            
            return distanceFromEndOfPath < 0.05f;
        }

        private IEnumerator EnableAgent()
        {
            while (!NavMeshSurface.activeSurfaces.FirstOrDefault()?.navMeshData)
            {
                yield return null;
            }
            _agent.enabled = true;
        }
    }
}