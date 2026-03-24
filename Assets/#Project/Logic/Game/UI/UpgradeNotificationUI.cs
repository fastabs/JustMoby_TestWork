using Lean.Transition;
using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class UpgradeNotificationUI : MonoBehaviour
    {
        [field: SerializeField] public LeanPlayer ShowTransition { get; private set; }

        private SignalBus _signalBus;
        private PlayerHealthService _playerHealth;

        [Inject]
        private void Construct(SignalBus signalBus, PlayerHealthService playerHealth)
        {
            _signalBus = signalBus;
            _playerHealth = playerHealth;
        }

        private void Awake()
        {
            _signalBus.Subscribe<FirstUpgradePointReceivedSignal>(OnFirstUpgradePointReceived);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<FirstUpgradePointReceivedSignal>(OnFirstUpgradePointReceived);
        }

        private void OnFirstUpgradePointReceived()
        {
            if (_playerHealth != null && _playerHealth.IsDead)
                return;

            ShowTransition?.Begin();
        }
    }
}
