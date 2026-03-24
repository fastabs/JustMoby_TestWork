using Lean.Transition;
using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class UpgradeNotificationUI : MonoBehaviour
    {
        [field: SerializeField] public LeanPlayer ShowTransition { get; private set; }

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<FirstUpgradePointReceivedSignal>(Show);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<FirstUpgradePointReceivedSignal>(Show);
        }

        private void Show()
        {
            ShowTransition?.Begin();
        }
    }
}
