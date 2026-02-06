using Enemy.Ai;
using Enemy.Ai.Humanoids;
using Enemy.Ai.Humanoids.MeleeAborigine;

namespace DiInstallers.Enemies
{
    public class MeleeAborigineAiInstaller : EnemyAiInstaller
    {
        protected override void InstallAi()
        {
            Container.BindInterfacesAndSelfTo<HumanoidAiContext>().AsSingle();
            Container.Bind<EnemyAiContext>().To<HumanoidAiContext>().FromResolve();
            
            Container.BindFactory<EnemyAiContext, EnemyFsm, MeleeAborigineFsmFactory>().AsSingle();
        }
    }
}