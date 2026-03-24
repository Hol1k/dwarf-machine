using Enemy.Ai;
using Enemy.Ai.Humanoids;
using Enemy.Ai.Humanoids.RangedAborigine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class RangedAborigineAiInstaller : EnemyAiInstaller
    {
        [Inject] private EnemyPatrolPointsCollection patrolPoints;
        [Inject] private EnemyRepositionPointsCollection repositionPoints;
        
        protected override void InstallAi()
        {
            Container.BindInterfacesAndSelfTo<RangedAborigineAiContext>().AsSingle();
            Container.Bind<EnemyAiContext>().To<RangedAborigineAiContext>().FromResolve();
            
            Container.BindFactory<EnemyAiContext, EnemyFsm, RangedAborigineFsmFactory>().AsSingle();
            
            Container.Bind<EnemyPatrolPointsCollection>().FromInstance(patrolPoints).AsSingle();
            Container.Bind<EnemyRepositionPointsCollection>().FromInstance(repositionPoints).AsSingle();
        }
    }
}