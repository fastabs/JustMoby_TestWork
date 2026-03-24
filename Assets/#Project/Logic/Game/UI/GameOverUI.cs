using Lean.Gui;
using Lean.Transition;
using TMPro;
using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class GameOverUI : MonoBehaviour
    {
        [field: SerializeField] public LeanPlayer ShowTransition { get; private set; }
        [field: SerializeField] public TextMeshProUGUI Label { get; private set; }
        [field: SerializeField] public LeanButton GoToMainMenuButton { get; private set; }
        [field: SerializeField] public PauseMenuUI PauseMenuUI { get; private set; }

        private string _labelTemplate;
        private bool _isGameOver;

        private ISceneService _sceneService;
        private IGameStatsService _gameStatsService;
        private ISaveEntryRepository _saveEntryRepository;
        private HealthParameter _playerHealthParameter;
        private SignalBus _signalBus;

        [Inject]
        private void Construct(SignalBus signalBus, ISceneService sceneService,
            IGameStatsService gameStatsService, ISaveEntryRepository saveEntryRepository,
            HealthParameter playerHealthParameter)
        {
            _signalBus = signalBus;
            _sceneService = sceneService;
            _gameStatsService = gameStatsService;
            _saveEntryRepository = saveEntryRepository;
            _playerHealthParameter = playerHealthParameter;
        }

        private void Awake()
        {
            if (Label != null)
                _labelTemplate = Label.text;

            if (GoToMainMenuButton != null)
                GoToMainMenuButton.OnClick.AddListener(OnGoToMainMenu);

            _signalBus.Subscribe<DeathSignal>(ShowGameOver);
        }

        private void OnDestroy()
        {
            _signalBus.Unsubscribe<DeathSignal>(ShowGameOver);

            if (GoToMainMenuButton != null)
                GoToMainMenuButton.OnClick.RemoveListener(OnGoToMainMenu);
        }

        private void ShowGameOver(DeathSignal signal)
        {
            if (signal.Health != _playerHealthParameter)
                return;

            if (_isGameOver)
                return;

            _isGameOver = true;

            if (Label != null)
            {
                var aliveTime = _gameStatsService.AliveTime;
                var killedEnemies = _gameStatsService.KilledEnemies;
                Label.text = string.Format(_labelTemplate, aliveTime, killedEnemies);
            }

            ShowTransition?.Begin();
            PauseMenuUI?.Hide();
            _saveEntryRepository.DeleteSave();
        }

        private void OnGoToMainMenu()
        {
            if (GoToMainMenuButton != null)
                GoToMainMenuButton.interactable = false;

            _sceneService.LoadMainMenu();
        }
    }
}
