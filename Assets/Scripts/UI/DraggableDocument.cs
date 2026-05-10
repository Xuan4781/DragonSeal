using UnityEngine;
using UnityEngine.EventSystems;

namespace DragonSeal.UI
{
    public class DraggableDocument : MonoBehaviour,
        IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        private RectTransform _rectTransform;
        private Canvas _canvas;
        private CanvasGroup _canvasGroup;
        private Vector2 _originalPosition;
        private bool _isDragging = false;

        [Header("Settings")]
        [SerializeField] private float dragAlpha = 0.85f;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();

            if (_canvasGroup == null)
                _canvasGroup = gameObject.AddComponent<CanvasGroup>();

            _originalPosition = _rectTransform.anchoredPosition;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            _isDragging = true;
            _canvasGroup.alpha = dragAlpha;

            // Bring to front
            transform.SetAsLastSibling();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_isDragging) return;

            _rectTransform.anchoredPosition += eventData.delta / _canvas.scaleFactor;
            Canvas.ForceUpdateCanvases();
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _isDragging = false;
            _canvasGroup.alpha = 1f;
        }

        public void ResetPosition()
        {
            _rectTransform.anchoredPosition = _originalPosition;
            _canvasGroup.alpha = 1f;
        }
    }
}
