using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DragonSeal.Core;

namespace DragonSeal.UI
{
    public class GameOverUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI subText;
        [SerializeField] private Button restartButton;
        [SerializeField] private Button mainMenuButton;

        private void Start()
        {
            titleText.text = "DISMISSED";
            subText.text = "Your trust rating hit zero.\nThe Bureau let you go.";

            restartButton.onClick.AddListener(OnRestartClicked);
            mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        }

        private void OnRestartClicked()
        {
            GameManager.Instance.RestartGame();
        }

        private void OnMainMenuClicked()
        {
            GameManager.Instance.GoToMainMenu();
        }
    }
}