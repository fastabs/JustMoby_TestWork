using UnityEngine;
using UnityEngine.UI;

namespace JustMoby_TestWork
{
    public sealed class MainMenuUI : MonoBehaviour
    {
        [field: SerializeField] public Button StartButton { get; private set; }
        [field: SerializeField] public Button ExitButton { get; private set; }
    }
}
