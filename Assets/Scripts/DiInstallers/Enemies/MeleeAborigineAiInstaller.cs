using Enemy;
using Enemy.Humanoids;
using Enemy.Humanoids.MeleeAborigine;

namespace DiInstallers.Enemies
{
    public class MeleeAborigineAiInstaller : EnemyAiInstaller
    {
        protected override void InstallAi()
        {
            Container.BindInterfacesAndSelfTo<HumanoidAiContext>().AsSingle();
            
            Container.BindFactory<HumanoidAiContext, EnemyFsm, MeleeAborigineFsmFactory>().AsSingle();
        }
    }
}