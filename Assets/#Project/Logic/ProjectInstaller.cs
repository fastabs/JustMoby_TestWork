using System;
using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class ProjectInstaller : MonoInstaller
    {
        [SerializeField] private GameConfiguration _gameConfiguration;
        [SerializeField] private Transform _playerTransform;

        public override void InstallBindings()
        {
            var inputActions = new InputActions();
            inputActions.Enable();

            if (_gameConfiguration == null)
                throw new InvalidOperationException("GameConfiguration is not assigned");

            if (_playerTransform == null)
                throw new InvalidOperationException("Player not found");

            Container.BindInstance(inputActions).AsSingle();
            Container.BindInstance(_gameConfiguration).AsSingle();
            Container.Bind<Transform>().FromInstance(_playerTransform).AsSingle();
            Container.BindInterfacesAndSelfTo<CursorService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputService>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerMovementService>().AsSingle();
        }
    }
}
