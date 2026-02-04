using Enemy.Ai;
using Enemy.Ai.Humanoids;
using Enemy.Ai.Humanoids.Soldier;

namespace DiInstallers.Enemies
{
    public class SoldierAiInstaller : EnemyAiInstaller
    {
        protected override void InstallAi()
        {
            Container.BindInterfacesAndSelfTo<HumanoidAiContext>().AsSingle();
            
            Container.BindFactory<HumanoidAiContext, EnemyFsm, SoldierFsmFactory>().AsSingle();
        }
    }
}