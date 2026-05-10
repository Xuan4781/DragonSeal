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

            // pick ending
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
            endingTitleText.text = "ENDING A\nThe Truth Bleeds Out";
            endingBodyText.text =
                "You flagged Old Man Zeth's file.\n\n" +
                "Within days, investigators pulled the thread.\n" +
                "The DSB's corruption unraveled publicly —\n" +
                "forced sealings, disappeared dissidents,\n" +
                "and at the center of it all: Ryuu.\n\n" +
                "Your father was arrested at the border.\n" +
                "He didn't resist.\n\n" +
                "You visited him once, through bulletproof glass.\n" +
                "He looked at your sword — five times your height —\n" +
                "and for the first time, he looked afraid.\n\n" +
                "You didn't say a word.\n" +
                "You didn't have to.";

            endingFooterText.text = "— Hana chose truth over silence.";
        }

        // ending 2
        private void ShowSilentEnding()
        {
            endingTitleText.text = "ENDING B\nThe Weight of Silence";
            endingBodyText.text =
                "You stamped Zeth's form and sent him on his way.\n\n" +
                "You told yourself it wasn't your place.\n" +
                "That one old man's testimony wouldn't change anything.\n" +
                "That you needed this job to survive.\n\n" +
                "Maybe all of that was true.\n\n" +
                "Ryuu was never found.\n" +
                "The DSB continued its work.\n" +
                "Citizens kept coming through your booth,\n" +
                "handing you their papers with trembling hands.\n\n" +
                "Every night you went home to an empty room\n" +
                "and leaned your sword against the wall —\n" +
                "five times your height,\n" +
                "and heavier every day.";

            endingFooterText.text = "— Hana chose survival over truth.";
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