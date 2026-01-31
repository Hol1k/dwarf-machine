using Enemy;
using Enemy.Humanoids;
using Enemy.Humanoids.RangedAborigine;

namespace DiInstallers.Enemies
{
    public class RangedAborigineAiInstaller : EnemyAiInstaller
    {
        protected override void InstallAi()
        {
            Container.BindInterfacesAndSelfTo<HumanoidAiContext>().AsSingle();
            
            Container.BindFactory<HumanoidAiContext, EnemyFsm, RangedAborigineFsmFactory>().AsSingle();
        }
    }
}