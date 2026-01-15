using UnityEngine;

namespace Enemy
{
    public class EnemyFsmContext
    {
        public EnemyFsmStateId? RequestedState = null;

        public float IdleTimer;

        public Vector3? PatrolPoint = null;
        
        public float LookingTimer;
        
        public Vector3? RepositionPoint;
    }
}