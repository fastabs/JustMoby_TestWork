using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class HealthParameter : IParameter<int>
    {
        public int Value { get; private set; }

        private readonly IStatsProvider _statsProvider;
        private readonly SignalBus _signalBus;

        public HealthParameter(IStatsProvider statsProvider, SignalBus signalBus)
        {
            _statsProvider = statsProvider;
            _signalBus = signalBus;
        }

        public void TakeDamage(int damage, Vector3 hitPoint)
        {
            var health = Value - damage;
            Value = health;

            if (health <= 0)
            {
                Value = 0;
                _signalBus.Fire(new DeathSignal(this));
            }

            _signalBus.Fire(new TakeDamageSignal(hitPoint, this));
        }

        public void TakeHeal(int heal)
        {
            SetDirectly(Value + heal);
        }

        public void SetDirectly(int health)
        {
            var maxHealth = _statsProvider.MaxHealth.Value;
            Value = Mathf.Clamp(health, 0, maxHealth);
            _signalBus.Fire(new TakeHealSignal(this));
        }
    }
}
