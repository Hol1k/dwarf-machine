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
            Container.Bind<HumanoidAiContext>().To<RangedAborigineAiContext>().FromResolve();
            
            Container.BindFactory<HumanoidAiContext, EnemyFsm, RangedAborigineFsmFactory>().AsSingle();
        }
    }
}