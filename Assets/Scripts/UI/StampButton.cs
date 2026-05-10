using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;
using DragonSeal.Core;

namespace DragonSeal.UI
{
    public class StampButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
    {
        [Header("Visuals")]
        [SerializeField] private Color normalColor;
        [SerializeField] private Color pressedColor;
        [SerializeField] private float pressOffset = 8f;

        [Header("Decision")]
        [SerializeField] private InspectionManager.StampDecision decision;

        private Image _image;
        private RectTransform _rectTransform;
        private Vector2 _originalPosition;
        private bool _isPressed = false;

        private void Awake()
        {
            _image = GetComponent<Image>();
            _rectTransform = GetComponent<RectTransform>();
            _originalPosition = _rectTransform.anchoredPosition;
            _image.color = normalColor;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_isPressed) return;
            _isPressed = true;

            _rectTransform.anchoredPosition = _originalPosition - new Vector2(0, pressOffset);
            _image.color = pressedColor;

            StartCoroutine(StampSequence());
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition = _originalPosition;
            _image.color = normalColor;
        }

        private IEnumerator StampSequence()
        {
            yield return new WaitForSeconds(0.15f);

            GameSceneUI gameSceneUI = FindObjectOfType<GameSceneUI>();
            if (gameSceneUI != null)
                gameSceneUI.OnStampApplied(decision);

            _isPressed = false;
        }
    }
}