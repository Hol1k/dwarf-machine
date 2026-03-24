using Enemy.Ai;
using Enemy.Ai.Humanoids;
using Enemy.Ai.Humanoids.MeleeAborigine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class MeleeAborigineAiInstaller : EnemyAiInstaller
    {
        [Inject] private EnemyPatrolPointsCollection patrolPoints;
        [Inject] private EnemyRepositionPointsCollection repositionPoints;
        
        protected override void InstallAi()
        {
            Container.BindInterfacesAndSelfTo<HumanoidAiContext>().AsSingle();
            Container.Bind<EnemyAiContext>().To<HumanoidAiContext>().FromResolve();
            
            Container.BindFactory<EnemyAiContext, EnemyFsm, MeleeAborigineFsmFactory>().AsSingle();
            
            Container.Bind<EnemyPatrolPointsCollection>().FromInstance(patrolPoints).AsSingle();
            Container.Bind<EnemyRepositionPointsCollection>().FromInstance(repositionPoints).AsSingle();
        }
    }
}