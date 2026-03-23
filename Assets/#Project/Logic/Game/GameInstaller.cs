using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class GameInstaller : MonoInstaller
    {
        [SerializeField] private Transform playerTransform;

        public override void InstallBindings()
        {
            Container.BindInstance(new InputActions()).AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovementService>().AsSingle().WithArguments(playerTransform);
            Container.BindInterfacesAndSelfTo<PlayerInputService>().AsSingle();
        }
    }
}
