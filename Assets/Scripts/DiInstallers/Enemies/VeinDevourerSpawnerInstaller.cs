using Enemy;
using Enemy.Ai;
using Enemy.SpawnManagers;
using UnityEngine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class VeinDevourerSpawnerInstaller : MonoInstaller
    {
        [SerializeField] private EnemyAiComponent veinDevourerPrefab;

        public override void InstallBindings()
        {
            Container
                .BindFactory<EnemyAiComponent, VeinDevourerFactory>()
                .FromSubContainerResolve()
                .ByNewContextPrefab(veinDevourerPrefab);
        }
    }
}