using Enemy;
using Enemy.Ai;
using Enemy.SpawnManagers;
using UnityEngine;
using Zenject;

namespace DiInstallers.Enemies
{
    public class AboriginesCampInstaller : MonoInstaller
    {
        [SerializeField] private EnemyAiComponent rangedAboriginePrefab;

        public override void InstallBindings()
        {
            Container
                .BindFactory<EnemyAiComponent, RangedAborigineFactory>()
                .FromComponentInNewPrefab(rangedAboriginePrefab);

            Container
                .Bind<IEnemyTeamController>()
                .To<AborigineTeamManager>()
                .AsSingle();
        }
    }
}