using System;
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