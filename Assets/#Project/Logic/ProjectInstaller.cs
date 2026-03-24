using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private ConfigRepository gameConfiguration;

        public override void InstallBindings()
        {
            Container.Bind<ConfigRepository>().FromInstance(gameConfiguration).AsSingle();
            Container.Bind<IConfigRepository>().FromInstance(gameConfiguration).AsSingle();
            Container.BindInterfacesAndSelfTo<CursorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SaveEntryRepository>().AsSingle();
        }
    }
}