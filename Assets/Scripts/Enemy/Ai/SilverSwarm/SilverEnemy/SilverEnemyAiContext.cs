using Enemy.Ai.AiContextInterfaces;

namespace Enemy.Ai.SilverSwarm.SilverEnemy
{
    public class SilverEnemyAiContext : EnemyAiContext, IAiSwarmDataAgent
    {
        public SilverEnemyAiContext(
            SilverSwarmAiContext dataAgent
        )
        {
            _dataAgent = dataAgent;
        }

        public bool AttackFlag { get; private set; }
        private IAiSwarmDataAgent _dataAgent;
    }
}