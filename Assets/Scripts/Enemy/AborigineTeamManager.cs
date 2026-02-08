using System.Collections.Generic;
using System.Linq;
using Enemy.Ai;
using Enemy.Ai.Humanoids.MeleeAborigine;
using Enemy.Ai.Humanoids.RangedAborigine;

namespace Enemy
{
    public class AborigineTeamManager : EnemyTeamManager, IAborigineTeamData
    {
        public AborigineTeamManager() {}
        public AborigineTeamManager(List<EnemyAiComponent> teamCollection) : base(teamCollection) {}
        
        public int MeleeCount => TeamCollection.Count(component => component is MeleeAborigineAiComponent);
        public int RangedCount => TeamCollection.Count(component => component is RangedAborigineAiComponent);
    }
}