using Enemy.Ai;
using Enemy.Ai.VeinDevourer;

namespace DiInstallers.Enemies
{
    public class VeinDevourerAiInstaller : EnemyAiInstaller
    {
        protected override void InstallAi()
        {
            Container.BindInterfacesAndSelfTo<VeinDevourerAiContext>().AsSingle();
            Container.Bind<EnemyAiContext>().To<VeinDevourerAiContext>().FromResolve();
            
            Container.BindFactory<EnemyAiContext, EnemyFsm, VeinDevourerFsmFactory>().AsSingle();
        }
    }
}