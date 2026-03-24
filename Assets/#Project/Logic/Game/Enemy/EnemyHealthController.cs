using System;
using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class EnemyHealthController : IHealthService, IInitializable, IDisposable
    {
        private static readonly int DeathTrigger = Animator.StringToHash("Death");
        private static readonly int TakeDamageTrigger = Animator.StringToHash("TakeDamage");
        private const float DESTROY_DELAY = 2f;

        private readonly IConfigRepository _config;
        private readonly IGameStatsService _gameStatsService;
        private readonly StatsUpgradeService _statsUpgradeService;
        private readonly Enemy _enemy;
        private readonly HealthParameter _healthParameter;
        private readonly SignalBus _signalBus;

        public EnemyHealthController(Enemy enemy, HealthParameter healthParameter, SignalBus signalBus,
            IConfigRepository config, IGameStatsService gameStatsService, StatsUpgradeService statsUpgradeService)
        {
            _enemy = enemy;
            _healthParameter = healthParameter;
            _signalBus = signalBus;
            _config = config;
            _gameStatsService = gameStatsService;
            _statsUpgradeService = statsUpgradeService;
        }

        public bool IsDead { get; private set; }

        public void Initialize()
        {
            _signalBus.Subscribe<TakeDamageSignal>(OnTakeDamage);
            _signalBus.Subscribe<DeathSignal>(OnDead);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<TakeDamageSignal>(OnTakeDamage);
            _signalBus.Unsubscribe<DeathSignal>(OnDead);
        }

        private void OnTakeDamage(TakeDamageSignal signal)
        {
            if (signal.Health != _healthParameter)
                return;

            _signalBus.Fire(new HitEnemySignal());

            if (!IsDead)
            {
                if (_enemy.Animator != null)
                    _enemy.Animator.SetTrigger(TakeDamageTrigger);
                return;
            }

            if (_enemy.Rigidbody != null)
            {
                var deathVelocityForce = _config.Enemy.DeathVelocityForce;
                var force = -_enemy.transform.forward * deathVelocityForce;
                _enemy.Rigidbody.AddForceAtPosition(force, signal.HitPoint, ForceMode.Impulse);
            }
        }

        private void OnDead(DeathSignal signal)
        {
            if (signal.Health != _healthParameter || IsDead)
                return;

            IsDead = true;
            if (_enemy.Animator != null)
                _enemy.Animator.SetTrigger(DeathTrigger);

            if (_enemy.Rigidbody != null)
            {
                _enemy.Rigidbody.useGravity = true;
                _enemy.Rigidbody.constraints = RigidbodyConstraints.None;
                _enemy.Rigidbody.velocity = Vector3.down;
            }

            UnityEngine.Object.Destroy(_enemy.gameObject, DESTROY_DELAY);

            _statsUpgradeService.AddAvailableUpgrade();
            _gameStatsService.AddEnemyKill();
        }
    }
}
