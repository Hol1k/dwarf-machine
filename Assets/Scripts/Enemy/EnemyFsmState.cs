namespace Enemy
{
    public abstract class EnemyFsmState
    {
        public abstract void Enter(EnemyFsmContext fsmContext);

        public abstract void Update(EnemyFsmContext fsmContext);

        public abstract void Exit(EnemyFsmContext fsmContext);
    }
}