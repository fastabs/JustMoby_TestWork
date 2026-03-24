using System;
using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class MainMenuService : IInitializable, IDisposable
    {
        private readonly MainMenuUI _mainMenuUI;
        private readonly ISceneService _sceneService;
        private readonly ISaveEntryRepository _saveEntryRepository;

        public MainMenuService(MainMenuUI mainMenuUI, ISceneService sceneService,
            ISaveEntryRepository saveEntryRepository)
        {
            _mainMenuUI = mainMenuUI;
            _sceneService = sceneService;
            _saveEntryRepository = saveEntryRepository;
        }

        public void Initialize()
        {
            _mainMenuUI.NewGameButton.OnClick.AddListener(OnStart);
            _mainMenuUI.ExitButton.OnClick.AddListener(OnExit);

            if (_mainMenuUI.LoadGameButton != null)
            {
                _mainMenuUI.LoadGameButton.interactable = _saveEntryRepository.IsSaveFileExists;
                _mainMenuUI.LoadGameButton.OnClick.AddListener(OnLoad);
            }
        }

        public void Dispose()
        {
            _mainMenuUI.NewGameButton.OnClick.RemoveListener(OnStart);
            _mainMenuUI.ExitButton.OnClick.RemoveListener(OnExit);

            if (_mainMenuUI.LoadGameButton != null)
                _mainMenuUI.LoadGameButton.OnClick.RemoveListener(OnLoad);
        }

        private void OnStart()
        {
            _saveEntryRepository.DeleteSave();
            _mainMenuUI.NewGameButton.interactable = false;
            _sceneService.LoadGame();
        }

        private void OnLoad()
        {
            if (!_saveEntryRepository.IsSaveFileExists)
                return;

            _saveEntryRepository.LoadSave();
            _mainMenuUI.LoadGameButton.interactable = false;
            _sceneService.LoadGame();
        }

        private void OnExit()
        {
            Application.Quit();
        }
    }
}