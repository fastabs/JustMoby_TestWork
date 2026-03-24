using Lean.Gui;
using Lean.Transition;
using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class PauseMenuUI : MonoBehaviour
    {
        [field: SerializeField] public LeanButton UpgradeStatsButton { get; private set; }
        [field: SerializeField] public LeanButton ExitToMainMenuButton { get; private set; }
        [field: SerializeField] public UpgradeStatsScreenUI UpgradeStatsScreenUI { get; private set; }
        [field: SerializeField] public LeanPlayer ShowTransition { get; private set; }
        [field: SerializeField] public LeanPlayer HideTransition { get; private set; }

        private ISaveGameService _saveGameService;
        private ISceneService _sceneService;
        private PauseService _pauseService;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(ISaveGameService saveGameService, ISceneService sceneService,
            SignalBus signalBus, PauseService pauseService)
        {
            _saveGameService = saveGameService;
            _sceneService = sceneService;
            _signalBus = signalBus;
            _pauseService = pauseService;
        }

        private void Awake()
        {
            if (UpgradeStatsButton != null)
                UpgradeStatsButton.OnClick.AddListener(OnUpgradeStatsClicked);

            if (ExitToMainMenuButton != null)
                ExitToMainMenuButton.OnClick.AddListener(OnExitToMainMenu);

            _signalBus?.Subscribe<PauseToggledSignal>(OnPauseToggled);
        }

        private void OnDestroy()
        {
            if (UpgradeStatsButton != null)
                UpgradeStatsButton.OnClick.RemoveListener(OnUpgradeStatsClicked);

            if (ExitToMainMenuButton != null)
                ExitToMainMenuButton.OnClick.RemoveListener(OnExitToMainMenu);

            _signalBus?.Unsubscribe<PauseToggledSignal>(OnPauseToggled);
        }

        public void Show()
        {
            ShowTransition?.Begin();
        }

        public void Hide()
        {
            HideTransition?.Begin();
        }

        private void OnPauseToggled(PauseToggledSignal _)
        {
            if (_pauseService.IsPaused)
                Show();
            else
                Hide();
        }

        private void OnUpgradeStatsClicked()
        {
            UpgradeStatsScreenUI?.Show();
        }

        private void OnExitToMainMenu()
        {
            _saveGameService.SaveGame();
            _sceneService.LoadMainMenu();
        }
    }
}