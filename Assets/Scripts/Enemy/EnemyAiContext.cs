using UnityEngine;
using Zenject;

namespace Enemy
{
    public class EnemyAiContext
    {
        public bool IsSeePlayer { get; private set; }
        public Vector3? LastSeePosition { get; set; }
        public Vector3 NextPatrolPoint => patrolComponent.GetNextPoint();

        [Inject] private EnemyPatrolComponent patrolComponent;
    }
}