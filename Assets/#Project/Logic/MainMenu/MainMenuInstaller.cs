using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class MainMenuInstaller : MonoInstaller
    {
        [SerializeField] private MainMenuUI mainMenuUI;

        public override void InstallBindings()
        {
            Container.BindInstance(mainMenuUI);
            Container.BindInterfacesAndSelfTo<MainMenuService>().AsSingle();
        }
    }
}