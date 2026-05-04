using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DragonSeal.Core;

namespace DragonSeal.UI
{
    public class MainMenuUI : MonoBehaviour
    {
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button quitButton;

        private void Start()
        {
            newGameButton.onClick.AddListener(OnNewGameClicked);
            quitButton.onClick.AddListener(OnQuitClicked);
        }

        private void OnNewGameClicked()
        {
            GameManager.Instance.StartNewGame();
        }

        private void OnQuitClicked()
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }

    }
}