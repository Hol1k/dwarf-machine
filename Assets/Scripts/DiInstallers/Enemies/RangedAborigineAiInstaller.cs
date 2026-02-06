using Enemy.Ai;
using Enemy.Ai.Humanoids;
using Enemy.Ai.Humanoids.RangedAborigine;

namespace DiInstallers.Enemies
{
    public class RangedAborigineAiInstaller : EnemyAiInstaller
    {
        protected override void InstallAi()
        {
            Container.BindInterfacesAndSelfTo<RangedAborigineAiContext>().AsSingle();
            Container.Bind<EnemyAiContext>().To<RangedAborigineAiContext>().FromResolve();
            
            Container.BindFactory<EnemyAiContext, EnemyFsm, RangedAborigineFsmFactory>().AsSingle();
        }
    }
}