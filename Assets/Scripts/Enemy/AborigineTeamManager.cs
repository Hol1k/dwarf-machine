using System.Linq;
using Enemy.Ai.Humanoids.MeleeAborigine;
using Enemy.Ai.Humanoids.RangedAborigine;

namespace Enemy
{
    public class AborigineTeamManager : EnemyTeamManager, IAborigineTeamData
    {
        public AborigineTeamManager() {}
        
        public int MeleeCount => TeamCollection.Count(component => component is MeleeAborigineAiComponent);
        public int RangedCount => TeamCollection.Count(component => component is RangedAborigineAiComponent);
    }
}