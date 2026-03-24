using Zenject;

namespace JustMoby_TestWork
{
    public sealed class PlayerHealthService : IHealthService, IInitializable
    {
        private readonly IPlayerLocator _playerLocator;
        private readonly ICursorService _cursorService;
        private readonly HealthParameter _playerHealth;
        private readonly SignalBus _signalBus;

        public bool IsDead { get; private set; }

        public PlayerHealthService(SignalBus signalBus, IPlayerLocator playerLocator,
            ICursorService cursorService, HealthParameter playerHealth)
        {
            _signalBus = signalBus;
            _playerLocator = playerLocator;
            _cursorService = cursorService;
            _playerHealth = playerHealth;
        }

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
            _cursorService.Show();
        }
    }
}
