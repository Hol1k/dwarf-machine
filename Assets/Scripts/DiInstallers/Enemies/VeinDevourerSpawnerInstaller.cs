using Enemy.Ai;
using Enemy.Spawners;
using UnityEngine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class VeinDevourerSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private EnemyAiComponent veinDevourerPrefab;

        public override void InstallBindings()
        {
            Container.Bind<VeinDevourerSpawner>().FromComponentOnRoot().AsSingle();
            Container
                .BindFactory<EnemyAiComponent, VeinDevourerFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(veinDevourerPrefab);
        }
    }
}