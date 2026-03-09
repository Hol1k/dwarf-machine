namespace Enemy.Ai.SilverSwarm.SilverEnemy
{
    public class SilverEnemyFsm : EnemyFsm
    {
        public SilverEnemyFsm(
            EnemyFsmState silverEnemyIdleState,
            EnemyFsmState silverEnemyPatrolState,
            EnemyFsmState silverEnemyCombatState)
        {
            States.Add(EnemyFsmStateId.Idle, silverEnemyIdleState);
            States.Add(EnemyFsmStateId.Patrol, silverEnemyPatrolState);
            States.Add(EnemyFsmStateId.Combat, silverEnemyCombatState);
            
            CurrentState = EnemyFsmStateId.Idle;
            States[CurrentState].Enter(FsmContext);
        }
    }
}