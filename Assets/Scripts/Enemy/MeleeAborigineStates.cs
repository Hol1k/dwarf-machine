namespace Enemy
{
    public static class MeleeAborigineStates
    {
        public static EnemyFsmState GetIdleState()
        {
            return new EnemyFsmState(null, null, null);
        }

        public static EnemyFsmState GetPatrolState()
        {
            return new EnemyFsmState(null, null, null);
        }

        public static EnemyFsmState GetCombatState()
        {
            return new EnemyFsmState(null, null, null);
        }

        public static EnemyFsmState GetAlertState()
        {
            return new EnemyFsmState(null, null, null);
        }

        public static EnemyFsmState GetRepositionState()
        {
            return new EnemyFsmState(null, null, null);
        }
    }
}