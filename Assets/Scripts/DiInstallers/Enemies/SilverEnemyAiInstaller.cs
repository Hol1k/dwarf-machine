using Enemy.Ai;
using Enemy.Ai.SilverSwarm;
using Enemy.Ai.SilverSwarm.SilverEnemy;
using Zenject;

namespace DiInstallers.Enemies
{
    public class SilverEnemyAiInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<EnemyAiComponent>().FromComponentsOnRoot().AsSingle();
        }
    }
}