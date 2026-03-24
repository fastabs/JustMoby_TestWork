using Lean.Gui;
using UnityEngine;

namespace JustMoby_TestWork
{
    public sealed class MainMenuUI : MonoBehaviour
    {
        [field: SerializeField] public LeanButton NewGameButton { get; private set; }
        [field: SerializeField] public LeanButton LoadGameButton { get; private set; }
        [field: SerializeField] public LeanButton ExitButton { get; private set; }
    }
}