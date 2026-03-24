using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class FirstUpgradePointNotificationUI : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI NotificationLabel { get; private set; }
        [field: SerializeField] public string Message { get; private set; } = "You got your first upgrade point!";
        [field: SerializeField] public float ShowDurationSeconds { get; private set; } = 3f;

        private SignalBus _signalBus;
        private Coroutine _showRoutine;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            if (NotificationLabel != null)
                NotificationLabel.gameObject.SetActive(false);

            _signalBus.Subscribe<FirstUpgradePointReceivedSignal>(Show);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<FirstUpgradePointReceivedSignal>(Show);
        }

        private void Show()
        {
            if (NotificationLabel == null)
                return;

            NotificationLabel.text = Message;
            NotificationLabel.gameObject.SetActive(true);

            if (_showRoutine != null)
                StopCoroutine(_showRoutine);

            _showRoutine = StartCoroutine(HideAfterDelay());
        }

        private IEnumerator HideAfterDelay()
        {
            yield return new WaitForSecondsRealtime(ShowDurationSeconds);

            if (NotificationLabel != null)
                NotificationLabel.gameObject.SetActive(false);

            _showRoutine = null;
        }
    }
}
