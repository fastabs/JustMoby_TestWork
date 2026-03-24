using Lean.Transition;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class PlayerHealthBarUI : MonoBehaviour
    {
        [field: SerializeField] public Slider Slider { get; private set; }
        [field: SerializeField] public TextMeshProUGUI HealthLabel { get; private set; }

        [field: Space]

        [field: SerializeField] public LeanPlayer OnDamageTransition { get; private set; }
        [field: SerializeField] public LeanPlayer OnHealTransition { get; private set; }
        [field: SerializeField] public LeanPlayer OnMaxHealthChangedTransition { get; private set; }

        private IPlayerStatsProvider _playerStatsProvider;
        private HealthParameter _playerHealthParameter;

        private SignalBus _signalBus;

        [Inject]
        private void Construct(IPlayerStatsProvider playerStatsProvider, HealthParameter playerHealthParameter,
            SignalBus signalBus)
        {
            _playerStatsProvider = playerStatsProvider;
            _playerHealthParameter = playerHealthParameter;
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<TakeDamageSignal>(OnTakeDamage);
            _signalBus.Subscribe<TakeHealSignal>(OnTakeHeal);
            _signalBus.Subscribe<ChangeMaxHealthSignal>(OnChangeMaxHealth);
            UpdateHealth();
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<TakeDamageSignal>(OnTakeDamage);
            _signalBus.Unsubscribe<TakeHealSignal>(OnTakeHeal);
            _signalBus.Unsubscribe<ChangeMaxHealthSignal>(OnChangeMaxHealth);
        }

        private void OnTakeDamage(TakeDamageSignal signal)
        {
            if (signal.Health != _playerHealthParameter)
                return;

            OnDamageTransition?.Begin();
            UpdateHealth();
        }

        private void OnTakeHeal(TakeHealSignal signal)
        {
            if (signal.Health != _playerHealthParameter)
                return;

            OnHealTransition?.Begin();
            UpdateHealth();
        }

        private void OnChangeMaxHealth()
        {
            OnMaxHealthChangedTransition?.Begin();

            var maxHealth = _playerStatsProvider.MaxHealth.Value;
            if (Slider != null)
                Slider.maxValue = maxHealth;

            _playerHealthParameter.SetDirectly(maxHealth);
            UpdateHealth();
        }

        private void UpdateHealth()
        {
            var health = _playerHealthParameter.Value;

            if (Slider != null)
                Slider.value = health;

            if (HealthLabel != null)
                HealthLabel.text = health.ToString();
        }
    }
}
