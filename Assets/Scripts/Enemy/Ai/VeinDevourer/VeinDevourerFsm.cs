namespace Enemy.Ai.VeinDevourer
{
    public class VeinDevourerFsm : EnemyFsm
    {
        public VeinDevourerFsm(
            EnemyFsmState idleState,
            EnemyFsmState combatState,
            EnemyFsmState repositionState,
            EnemyFsmState patrolState,
            EnemyFsmState eatingState)
        {
            States.Add(EnemyFsmStateId.Idle,  idleState);
            States.Add(EnemyFsmStateId.Combat,  combatState);
            States.Add(EnemyFsmStateId.Reposition,  repositionState);
            States.Add(EnemyFsmStateId.Patrol,  patrolState);
            States.Add(EnemyFsmStateId.Eating,  eatingState);
            
            CurrentState = EnemyFsmStateId.Idle;
            States[CurrentState].Enter(FsmContext);
        }
    }
}