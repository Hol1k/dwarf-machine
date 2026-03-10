using UnityEngine;

namespace Enemy.Ai.AiContextInterfaces
{
    public interface IAiSwarmDataAgent
    {
        bool AttackFlag { get; }
        bool IsPointInsideSwarm(Vector3 fsmContextPatrolPoint);
        Vector3 GetPointInsideSwarm { get; }
        Vector3? TargetPosition { get; }
        Vector3? GetPointBehindTarget { get; }
    }
}