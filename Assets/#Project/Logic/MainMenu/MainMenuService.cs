using UnityEngine;
using Zenject;

namespace JustMoby_TestWork
{
    public sealed class MainMenuService : IInitializable
    {
        private readonly MainMenuUI _mainMenuUI;
        private readonly ISceneService _sceneService;

        public MainMenuService(MainMenuUI mainMenuUI, ISceneService sceneService)
        {
            _mainMenuUI = mainMenuUI;
            _sceneService = sceneService;
        }

        public void Initialize()
        {
            _mainMenuUI.StartButton.onClick.AddListener(OnStart);
            _mainMenuUI.ExitButton.onClick.AddListener(OnExit);
        }

        private void OnStart()
        {
            _mainMenuUI.StartButton.interactable = false;
            _sceneService.LoadGame();
        }

        private void OnExit()
        {
            Application.Quit();
        }
    }
}