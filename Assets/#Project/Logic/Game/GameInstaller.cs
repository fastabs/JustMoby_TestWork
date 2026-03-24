using System;
using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private Player player;
        [SerializeField] private PrefabsLibrary prefabsLibrary;

        public override void InstallBindings()
        {
            player ??= SceneObjectLocator.FindInScene<Player>(gameObject.scene);
            if (player == null)
                throw new InvalidOperationException($"{nameof(GameInstaller)} requires a Player in the Game scene.");

            Container.BindInstance(new InputActions()).AsSingle();
            Container.BindInstance(player).AsSingle();
            Container.QueueForInject(player);

            if (prefabsLibrary != null)
                Container.BindInstance(prefabsLibrary).AsSingle();

            BindSignals();

            Container.BindInterfacesAndSelfTo<PauseService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStatsService>().AsSingle();
            Container.BindPlayerCore();

            Container.BindInterfacesAndSelfTo<ProjectileFactory>().AsSingle();
            Container.Bind<IPlayerShootingService>().To<PlayerShootingService>().AsSingle();

            Container.BindInterfacesAndSelfTo<EnemySpawnController>().AsSingle();

            Container.BindInterfacesAndSelfTo<PlayerInputController>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveGameService>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameInitService>().AsSingle();
        }

        private void BindSignals()
        {
            SignalBusInstaller.Install(Container);

            Container.DeclareSignal<PauseToggledSignal>();
            Container.DeclareSignal<StatsUpgradesCountSignal>();
            Container.DeclareSignal<TakeDamageSignal>();
            Container.DeclareSignal<TakeHealSignal>();
            Container.DeclareSignal<ChangeMaxHealthSignal>();
            Container.DeclareSignal<DeathSignal>();
            Container.DeclareSignal<HitEnemySignal>();
            Container.DeclareSignal<EnemySpawnTimerSignal>();
            Container.DeclareSignal<UpgradeAppliedSignal>();
            Container.DeclareSignal<UpgradeCanceledSignal>();
            Container.DeclareSignal<StatUpgradeValueSignal>();
            Container.DeclareSignal<StatUpgradeCreatedSignal>();
            Container.DeclareSignal<FirstUpgradePointReceivedSignal>();
        }
    }
}
