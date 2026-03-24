using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class PlayerHealthService : IHealthService, IInitializable
    {
        private static readonly int DeathTrigger = Animator.StringToHash("Death");

        private readonly Player _player;
        private readonly IPlayerLocator _playerLocator;
        private readonly ICursorService _cursorService;
        private readonly HealthParameter _playerHealth;
        private readonly SignalBus _signalBus;

        public PlayerHealthService(SignalBus signalBus, IPlayerLocator playerLocator,
            ICursorService cursorService, Player player, HealthParameter playerHealth)
        {
            _signalBus = signalBus;
            _playerLocator = playerLocator;
            _cursorService = cursorService;
            _player = player;
            _playerHealth = playerHealth;
        }

        public bool IsDead { get; private set; }

        public void Initialize()
        {
            _signalBus.Subscribe<DeathSignal>(OnDead);
        }

        private void OnDead(DeathSignal signal)
        {
            if (signal.Health != _playerHealth)
                return;

            IsDead = true;
            _playerLocator.SetupPlayer(null);
            if (_player.Animator != null)
                _player.Animator.SetTrigger(DeathTrigger);
            _cursorService.Show();
        }
    }
}
