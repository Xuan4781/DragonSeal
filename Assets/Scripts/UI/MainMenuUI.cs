using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using DragonSeal.Core;

namespace DragonSeal.UI
{
    public class MainMenuUI : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [SerializeField] private Button newGameButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private float pressOffset = 8f;

        private RectTransform _newGameRect;
        private RectTransform _quitRect;
        private Vector2 _newGameOriginalPos;
        private Vector2 _quitOriginalPos;

        private void Start()
        {
            newGameButton.onClick.AddListener(OnNewGameClicked);
            quitButton.onClick.AddListener(OnQuitClicked);

            _newGameRect = newGameButton.GetComponent<RectTransform>();
            _quitRect = quitButton.GetComponent<RectTransform>();

            _newGameOriginalPos = _newGameRect.anchoredPosition;
            _quitOriginalPos = _quitRect.anchoredPosition;

            AddPressAnimation(newGameButton, _newGameRect, _newGameOriginalPos);
            AddPressAnimation(quitButton, _quitRect, _quitOriginalPos);
        }

        private void AddPressAnimation(Button button, RectTransform rect, Vector2 originalPos)
        {
            EventTrigger trigger = button.gameObject.AddComponent<EventTrigger>();

            // Press down
            EventTrigger.Entry downEntry = new EventTrigger.Entry();
            downEntry.eventID = EventTriggerType.PointerDown;
            downEntry.callback.AddListener((data) =>
            {
                rect.anchoredPosition = originalPos - new Vector2(0, pressOffset);
            });
            trigger.triggers.Add(downEntry);

            // Release
            EventTrigger.Entry upEntry = new EventTrigger.Entry();
            upEntry.eventID = EventTriggerType.PointerUp;
            upEntry.callback.AddListener((data) =>
            {
                rect.anchoredPosition = originalPos;
            });
            trigger.triggers.Add(upEntry);
        }

        public void OnPointerDown(PointerEventData eventData) { }
        public void OnPointerUp(PointerEventData eventData) { }

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