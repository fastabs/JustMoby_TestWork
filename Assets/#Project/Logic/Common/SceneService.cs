using UnityEngine.SceneManagement;

namespace JustMoby_TestWork
{
    public interface ISceneService
    {
        void LoadMainMenu();
        void LoadGame();
    }

    public sealed class SceneService : ISceneService
    {
        public void LoadMainMenu()
        {
            SceneManager.LoadSceneAsync(SceneNames.MainMenu, LoadSceneMode.Single);
        }

        public void LoadGame()
        {
            SceneManager.LoadSceneAsync(SceneNames.Game, LoadSceneMode.Single);
        }
    }

    public static class SceneNames
    {
        public const string MainMenu = "MainMenu";
        public const string Game = "Game";
    }
}
