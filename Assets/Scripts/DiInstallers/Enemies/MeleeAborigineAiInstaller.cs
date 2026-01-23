using Enemy;
using Enemy.MeleeAborigine;

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