using Enemy;
using Enemy.Ai;
using Enemy.Ai.Humanoids;
using Enemy.Ai.Humanoids.MeleeAborigine;
using Enemy.Humanoids;

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