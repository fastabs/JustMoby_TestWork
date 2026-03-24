using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public interface IEnemyTargetDetector
    {
        Transform Target { get; }
    }

    public sealed class EnemyTargetDetectorController : IEnemyTargetDetector, ITickable
    {
        public Transform Target { get; private set; }

        private readonly Enemy _enemy;
        private readonly IPlayerLocator _playerLocator;

        public EnemyTargetDetectorController(IPlayerLocator playerLocator, Enemy enemy)
        {
            _playerLocator = playerLocator;
            _enemy = enemy;
        }

        public void Tick()
        {
            DetectPlayer();
        }

        private void DetectPlayer()
        {
            if (_playerLocator.Player == null)
            {
                Target = null;
                return;
            }

            var player = _playerLocator.Player.transform;
            var transform = _enemy.transform;

            if (!Physics.Raycast(transform.position, player.position - transform.position, out var hit))
            {
                Target = null;
                return;
            }

            Target = hit.transform == player ? hit.transform : null;
        }
    }
}
