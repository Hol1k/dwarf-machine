using System.Collections.Generic;
using Enemy.Ai;

namespace Enemy
{
    public class EnemyTeamManager : IEnemyTeamController, IEnemyTeamData
    {
        private readonly List<EnemyAiComponent> _teamCollection = new();
        
        public EnemyTeamManager() {}
        
        public List<EnemyAiComponent> TeamCollection => _teamCollection;

        public int TeamCount => _teamCollection.Count;
    }
}