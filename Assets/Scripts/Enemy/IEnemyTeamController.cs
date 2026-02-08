using System.Collections.Generic;
using Enemy.Ai;

namespace Enemy
{
    public interface IEnemyTeamController
    {
        List<EnemyAiComponent> TeamCollection { get; }
    }
}