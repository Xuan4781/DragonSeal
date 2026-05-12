using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DragonSeal.Core;

namespace DragonSeal.UI
{
    public class DayEndUI : MonoBehaviour
    {
        [Header("UI Elements")]
        [SerializeField] private TextMeshProUGUI dayEndTitleText;
        [SerializeField] private TextMeshProUGUI trustText;
        [SerializeField] private TextMeshProUGUI storyEventText;
        [SerializeField] private Button continueButton;
        [SerializeField] private Button mainMenuButton;

        private string[] _storyEvents = new string[]
        {
            // d1
            "As you close up the booth, you notice an old bulletin board.\n" +
            "A wanted poster catches your eye — the name is redacted,\n" +
            "but the dragon class reads: CLASS S.\n" +
            "You haven't seen that classification before.",
            // d2
            "Tonight's government memo sits on your desk.\n" +
            "'Fugitive dragon-powered individual at large. Extremely dangerous.\n" +
            "Last seen near the capital. Do not engage.'\n" +
            "The description feels familiar. You can't explain why.",
            // d3
            "You sit alone in the empty booth long after closing.\n" +
            "Everything Zeth told you plays back in your mind.\n" +
            "Your father didn't just kill your mother.\n" +
            "He did it to protect a system built on lies.\n\n" +
            "To be continued..."
        };

        private void Start()
        {
            if (GameManager.Instance == null) return;

            int day = GameManager.Instance.DayNumber;
            int trust = GameManager.Instance.TrustRating;

            dayEndTitleText.text = $"END OF DAY {day}";
            trustText.text = $"TRUST RATING: {trust}";

            int storyIndex = Mathf.Clamp(day - 1, 0, _storyEvents.Length - 1);
            storyEventText.text = _storyEvents[storyIndex];

            continueButton.onClick.AddListener(OnContinueClicked);
            mainMenuButton.onClick.AddListener(OnMainMenuClicked);
        }

        private void OnContinueClicked()
        {
            if (GameManager.Instance.DayNumber == 3)
                GameManager.Instance.GoToEnding();
            else
                GameManager.Instance.StartNextDay();
        }

        private void OnMainMenuClicked()
        {
            GameManager.Instance.GoToMainMenu();
        }
    }
}