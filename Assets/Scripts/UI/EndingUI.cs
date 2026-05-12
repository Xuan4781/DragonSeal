using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DragonSeal.Core;

namespace DragonSeal.UI
{
    public class EndingUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI endingTitleText;
        [SerializeField] private TextMeshProUGUI endingBodyText;
        [SerializeField] private TextMeshProUGUI endingFooterText;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button mainMenuButton;

        private void Start()
        {
            if (GameManager.Instance == null) return;

            switch (GameManager.Instance.CurrentEnding)
            {
                case GameManager.EndingType.ExposedTruth:
                    ShowExposedEnding();
                    break;
                case GameManager.EndingType.StayedSilent:
                    ShowSilentEnding();
                    break;
                default:
                    ShowSilentEnding();
                    break;
            }

            playAgainButton.onClick.AddListener(OnPlayAgainClicked);
            mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        }

        // ending 1
        private void ShowExposedEnding()
        {
            endingTitleText.text = "ENDING A\nWhat is next?";
            endingBodyText.text =
                "You rejected Zeth.\n\n" +
                "His file was perfectly valid, you rejected him out of what?\n" +
                "The DSB's corruption unraveled, there was more behind the scenes. —\n" +
                "forced sealings, disappeared dissidents,\n" +
                "and of course that means more dangers lies ahead.";

            endingFooterText.text = "Hana didn't want to take a risk.";
        }

        // ending 2
        private void ShowSilentEnding()
        {
            endingTitleText.text = "ENDING B\nWhat do we do now?";
            endingBodyText.text =
                "You approved Zeth's form and sent him on his way.\n\n" +
                "You told yourself it's just a job.\n" +
                "That man's threat wouldn't change anything.";

            endingFooterText.text = "Hana just wants to get on her day.";
        }

        private void OnPlayAgainClicked()
        {
            GameManager.Instance.RestartGame();
        }

        private void OnMainMenuClicked()
        {
            GameManager.Instance.GoToMainMenu();
        }
    }
}