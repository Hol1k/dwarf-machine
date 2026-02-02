using Enemy;
using Enemy.Ai;
using Enemy.Ai.Humanoids;
using Enemy.Ai.Humanoids.RangedAborigine;
using Enemy.Humanoids;

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