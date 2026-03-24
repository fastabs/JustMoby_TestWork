using Lean.Transition;
using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class Crosshair : MonoBehaviour
    {
        [field: SerializeField] public LeanPlayer HitTransition { get; private set; }

        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        private void Awake()
        {
            _signalBus.Subscribe<HitEnemySignal>(ShowHit);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<HitEnemySignal>(ShowHit);
        }

        public void ShowHit()
        {
            HitTransition?.Begin();
        }
    }
}
