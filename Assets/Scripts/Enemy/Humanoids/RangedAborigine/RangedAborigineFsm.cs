namespace Enemy.Humanoids.RangedAborigine
{
    public class RangedAborigineFsm : EnemyFsm
    {
        public RangedAborigineFsm(
            EnemyFsmState idleState, 
            EnemyFsmState patrolState, 
            EnemyFsmState combatState, 
            EnemyFsmState alertState, 
            EnemyFsmState repositionState)
        {
            States.Add(EnemyFsmStateId.Idle,  idleState);
            States.Add(EnemyFsmStateId.Patrol,  patrolState);
            States.Add(EnemyFsmStateId.Combat,  combatState);
            States.Add(EnemyFsmStateId.Alert,  alertState);
            States.Add(EnemyFsmStateId.Reposition,  repositionState);
            
            CurrentState = EnemyFsmStateId.Idle;
            States[CurrentState].Enter(FsmContext);
        }
    }
}