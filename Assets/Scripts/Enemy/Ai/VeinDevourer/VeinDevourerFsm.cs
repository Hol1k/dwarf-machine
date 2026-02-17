namespace Enemy.Ai.VeinDevourer
{
    public class VeinDevourerFsm : EnemyFsm
    {
        public VeinDevourerFsm(
            EnemyFsmState idleState,
            EnemyFsmState combatState,
            EnemyFsmState repositionState)
        {
            States.Add(EnemyFsmStateId.Idle,  idleState);
            States.Add(EnemyFsmStateId.Combat,  combatState);
            States.Add(EnemyFsmStateId.Reposition,  repositionState);
            
            CurrentState = EnemyFsmStateId.Idle;
            States[CurrentState].Enter(FsmContext);
        }
    }
}