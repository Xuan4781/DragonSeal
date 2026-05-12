using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DragonSeal.Core;
using DragonSeal.Data;
using System.Collections;

namespace DragonSeal.UI
{
    public class GameSceneUI : MonoBehaviour
    {
        [Header("Citizen Panel")]
        [SerializeField] private Image citizenPortrait;
        [SerializeField] private TextMeshProUGUI citizenNameText;
        [SerializeField] private TextMeshProUGUI citizenDialogueText;
        [SerializeField] private Button talkButton;

        [Header("Document Cards")]
        [SerializeField] private DocumentCard citizenDocumentCard;
        [SerializeField] private DocumentCard governmentDatabaseCard;
        [SerializeField] private DraggableDocument citizenDocumentDraggable;
        [SerializeField] private DraggableDocument governmentDatabaseDraggable;

        [Header("Decision Panel")]
        [SerializeField] private TextMeshProUGUI trustText;
        [SerializeField] private TextMeshProUGUI dayText;
        [SerializeField] private TextMeshProUGUI warningText;

        private void Awake()
        {
            InspectionManager.Instance.OnCitizenArrived += UpdateCitizenUI;
            InspectionManager.Instance.OnDecisionMade += OnDecisionMade;
        }

        private void Start()
        {
            talkButton.onClick.AddListener(OnTalkClicked);
            warningText.text = "";

            UpdateDayAndTrust();

            CitizenSO first = InspectionManager.Instance.GetCurrentCitizen();
            if (first != null) UpdateCitizenUI(first);
        }

        private void OnDestroy()
        {
            if (InspectionManager.Instance != null)
            {
                InspectionManager.Instance.OnCitizenArrived -= UpdateCitizenUI;
                InspectionManager.Instance.OnDecisionMade -= OnDecisionMade;
            }
        }

        private void UpdateCitizenUI(CitizenSO citizen)
        {
            citizenNameText.text = citizen.citizenName;
            citizenDialogueText.text = "...";

            if (!string.IsNullOrEmpty(citizen.portraitKey))
            {
                Sprite loaded = JsonImageLoader.Instance.GetPortrait(citizen.portraitKey);
                citizenPortrait.sprite = loaded != null ? loaded : citizen.portrait;
            }
            else if (citizen.portrait != null)
                citizenPortrait.sprite = citizen.portrait;

            citizenDocumentCard.LoadAsDocument(citizen);
            governmentDatabaseCard.LoadAsDatabase(citizen);

            warningText.text = "";
            UpdateDayAndTrust();
        }
        

        public void OnStampApplied(InspectionManager.StampDecision decision)
        {
            if (!citizenDocumentCard.IsViewed || !governmentDatabaseCard.IsViewed)
            {
                warningText.text = "? Review both documents before stamping!";
                warningText.color = Color.yellow;
                return;
            }

            warningText.text = "";
            citizenDocumentCard.ApplyStamp(decision);
            StartCoroutine(ProceedAfterStamp(decision));
        }

        private IEnumerator ProceedAfterStamp(InspectionManager.StampDecision decision)
        {
            yield return new WaitForSeconds(1.2f);
            InspectionManager.Instance.MakeDecision(decision);
        }

        private void OnDecisionMade(InspectionManager.StampDecision decision)
        {
            UpdateDayAndTrust();
        }

        private void UpdateDayAndTrust()
        {
            if (GameManager.Instance == null) return;
            trustText.text = $"TRUST: {GameManager.Instance.TrustRating}";
            dayText.text = $"DAY {GameManager.Instance.DayNumber}";
        }

        private void OnTalkClicked()
        {
            CitizenSO current = InspectionManager.Instance.GetCurrentCitizen();
            if (current == null) return;

            citizenDialogueText.text = "...";
            talkButton.interactable = false;

            OpenAIService.Instance.GetCitizenDialogue(current, (dialogue) =>
            {
                citizenDialogueText.text = dialogue;
                talkButton.interactable = true;
            });
        }
    }
}