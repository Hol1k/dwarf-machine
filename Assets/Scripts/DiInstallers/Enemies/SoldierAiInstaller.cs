using Enemy.Ai;
using Enemy.Ai.Humanoids;
using Enemy.Ai.Humanoids.Soldier;

namespace DiInstallers.Enemies
{
    public class SoldierAiInstaller : EnemyAiInstaller
    {
        protected override void InstallAi()
        {
            Container.BindInterfacesAndSelfTo<HumanoidAiContext>().AsSingle();
            Container.Bind<EnemyAiContext>().To<HumanoidAiContext>().FromResolve();
            
            Container.BindFactory<EnemyAiContext, EnemyFsm, SoldierFsmFactory>().AsSingle();
        }
    }
}