using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameConfiguration gameConfiguration;

        public override void InstallBindings()
        {
            Container.BindInstance(gameConfiguration).AsSingle();
            Container.BindInterfacesAndSelfTo<CursorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<SceneService>().AsSingle();
        }
    }
}
