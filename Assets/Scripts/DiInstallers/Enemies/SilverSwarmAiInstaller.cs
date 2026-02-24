using Enemy.Ai;
using Enemy.Ai.SilverSwarm;
using Enemy.Ai.SilverSwarm.SilverEnemy;

namespace DiInstallers.Enemies
{
    public class SilverSwarmAiInstaller : EnemyAiInstaller
    {
        protected override void InstallAi()
        {
            Container.BindInterfacesAndSelfTo<SilverSwarmAiContext>().AsSingle();
            Container.Bind<EnemyAiContext>().To<SilverSwarmAiContext>().FromResolve();
            
            Container.BindFactory<EnemyAiContext, EnemyFsm, SilverSwarmFsmFactory>().AsSingle();
            Container.BindFactory<EnemyAiContext, EnemyFsm, SilverEnemyFsmFactory>().AsSingle();
        }
    }
}