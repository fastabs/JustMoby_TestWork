using Zenject;

namespace JustMoby_TestWork
{
    public sealed class PauseService
    {
        public bool IsPaused { get; private set; }

        private readonly SignalBus _signalBus;

        public PauseService(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Pause()
        {
            IsPaused = true;
            _signalBus.Fire(new PauseToggledSignal());
        }

        public void Unpause()
        {
            IsPaused = false;
            _signalBus.Fire(new PauseToggledSignal());
        }

        public void Toggle()
        {
            if (IsPaused)
                Unpause();
            else
                Pause();
        }
    }
}