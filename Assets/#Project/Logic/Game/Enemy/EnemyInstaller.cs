using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class EnemyInstaller : MonoInstaller
    {
        [SerializeField] private Enemy enemy;

        public override void InstallBindings()
        {
            Container.BindInstance(enemy).AsSingle();
            Container.QueueForInject(enemy);

            Container.BindInterfacesAndSelfTo<EnemyStatsProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<HealthParameter>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyTargetDetectorController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyHealthController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyMovementController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyShootingController>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnemyInitService>().AsSingle();
        }
    }
}
