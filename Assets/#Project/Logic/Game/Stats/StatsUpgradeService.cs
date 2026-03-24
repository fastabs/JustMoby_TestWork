using System;
using System.Collections.Generic;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class StatsUpgradeService : IInitializable
    {
        public int AvailableUpgradeCount { get; private set; }

        private int _currentAvailableUpgradeCount;
        public int CurrentAvailableUpgradeCount
        {
            get => _currentAvailableUpgradeCount;
            private set
            {
                _currentAvailableUpgradeCount = value;
                _signalBus.Fire(new StatsUpgradesCountSignal(value));
            }
        }

        private readonly IPlayerStatsProvider _playerStatsProvider;
        private readonly SignalBus _signalBus;

        private List<StatUpgrade> _statUpgrades;

        public StatsUpgradeService(IPlayerStatsProvider playerStatsProvider, SignalBus signalBus)
        {
            _playerStatsProvider = playerStatsProvider;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _statUpgrades = new List<StatUpgrade>();
            CreateUpgrade(_playerStatsProvider.MaxHealth);
            CreateUpgrade(_playerStatsProvider.MoveSpeed);
            CreateUpgrade(_playerStatsProvider.Damage);
            _signalBus.Fire(new StatsUpgradesCountSignal(CurrentAvailableUpgradeCount));
        }

        public void AddAvailableUpgrade(int count = 1, bool notifyFirstUpgrade = true)
        {
            var wasWithoutUpgrades = AvailableUpgradeCount <= 0;
            AvailableUpgradeCount += count;
            CurrentAvailableUpgradeCount += count;

            if (notifyFirstUpgrade && count > 0 && wasWithoutUpgrades && AvailableUpgradeCount > 0)
                _signalBus.Fire(new FirstUpgradePointReceivedSignal());
        }

        public bool TryChangeStat(Stat stat, int delta)
        {
            if (delta > 0 && CurrentAvailableUpgradeCount <= 0)
                return false;

            var statUpgradeIndex = GetStatUpgradeIndex(stat);
            var statUpgrade = _statUpgrades[statUpgradeIndex];

            var upgradeLevel = statUpgrade.NewUpgradeLevel + delta;
            if (!stat.CanSetUpgrade(upgradeLevel))
                return false;

            _statUpgrades[statUpgradeIndex].NewUpgradeLevel = upgradeLevel;
            CurrentAvailableUpgradeCount -= delta;
            _signalBus.Fire(new StatUpgradeValueSignal(stat, statUpgrade.NewUpgradeLevel));

            return true;
        }

        public void ApplyUpgrade()
        {
            foreach (var statUpgrade in _statUpgrades)
            {
                var stat = statUpgrade.Stat;
                var currentUpgradeLevel = stat.CurrentUpgradeLevel;
                stat.SetUpgradeLevel(statUpgrade.NewUpgradeLevel);

                if (stat is MaxHealthStat && currentUpgradeLevel != statUpgrade.NewUpgradeLevel)
                    _signalBus.Fire(new ChangeMaxHealthSignal());
            }

            AvailableUpgradeCount = CurrentAvailableUpgradeCount;
            Reset();
            _signalBus.Fire(new UpgradeAppliedSignal());
        }

        public void CancelUpgrade()
        {
            Reset();
            _signalBus.Fire(new UpgradeCanceledSignal());
        }

        private void CreateUpgrade(Stat stat)
        {
            _statUpgrades.Add(new StatUpgrade
            {
                Stat = stat,
                NewUpgradeLevel = stat.CurrentUpgradeLevel
            });

            _signalBus.Fire(new StatUpgradeCreatedSignal(stat));
        }

        private void Reset()
        {
            foreach (var statUpgrade in _statUpgrades)
            {
                var level = statUpgrade.Stat.CurrentUpgradeLevel;
                statUpgrade.NewUpgradeLevel = level;
                _signalBus.Fire(new StatUpgradeValueSignal(statUpgrade.Stat, level));
            }

            CurrentAvailableUpgradeCount = AvailableUpgradeCount;
        }

        private int GetStatUpgradeIndex(Stat stat)
        {
            for (var i = 0; i < _statUpgrades.Count; i++)
            {
                if (_statUpgrades[i].Stat == stat)
                    return i;
            }

            throw new InvalidOperationException($"StatUpgrade {stat.GetType().Name} not found.");
        }

        private sealed class StatUpgrade
        {
            public Stat Stat;
            public int NewUpgradeLevel;
        }
    }
}
