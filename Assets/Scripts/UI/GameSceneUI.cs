using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DragonSeal.Core;
using DragonSeal.Data;

namespace DragonSeal.UI
{
    public class GameSceneUI : MonoBehaviour
    {
        [Header("Citizen Panel")]
        [SerializeField] private Image citizenPortrait;
        [SerializeField] private TextMeshProUGUI citizenNameText;
        [SerializeField] private TextMeshProUGUI citizenDialogueText;
        [SerializeField] private Button talkButton;

        [Header("Document Panel")]
        [SerializeField] private TextMeshProUGUI docNameText;
        [SerializeField] private TextMeshProUGUI docClassText;
        [SerializeField] private TextMeshProUGUI docForgedText;
        [SerializeField] private TextMeshProUGUI scannerResultText;
        [SerializeField] private Image scannerDisplay;

        [Header("Decision Panel")]
        [SerializeField] private TextMeshProUGUI trustText;
        [SerializeField] private TextMeshProUGUI dayText;
        [SerializeField] private Button approveButton;
        [SerializeField] private Button rejectButton;
        [SerializeField] private Button flagButton;

        private void Awake()
        {
            InspectionManager.Instance.OnCitizenArrived += UpdateCitizenUI;
            InspectionManager.Instance.OnDecisionMade += OnDecisionMade;
        }

        private void Start()
        {
            approveButton.onClick.AddListener(() =>
                InspectionManager.Instance.MakeDecision(InspectionManager.StampDecision.Approve));
            rejectButton.onClick.AddListener(() =>
                InspectionManager.Instance.MakeDecision(InspectionManager.StampDecision.Reject));
            flagButton.onClick.AddListener(() =>
                InspectionManager.Instance.MakeDecision(InspectionManager.StampDecision.Flag));

            talkButton.onClick.AddListener(OnTalkClicked);

            UpdateDayAndTrust();

            CitizenSO firstCitizen = InspectionManager.Instance.GetCurrentCitizen();
            if (firstCitizen != null)
                UpdateCitizenUI(firstCitizen);
        }

        private void OnDestroy()
        {
            if (InspectionManager.Instance != null)
            {
                InspectionManager.Instance.OnCitizenArrived -= UpdateCitizenUI;
                InspectionManager.Instance.OnDecisionMade -= OnDecisionMade;
            }
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

        private void UpdateCitizenUI(CitizenSO citizen)
        {
            citizenNameText.text = citizen.citizenName;
            citizenDialogueText.text = "...";

            // json loader
            if (!string.IsNullOrEmpty(citizen.portraitKey))
            {
                Sprite loaded = JsonImageLoader.Instance.GetPortrait(citizen.portraitKey);
                citizenPortrait.sprite = loaded != null ? loaded : citizen.portrait;
            }
            else if (citizen.portrait != null)
            {
                citizenPortrait.sprite = citizen.portrait;
            }

            // doc
            docNameText.text = $"Name: {citizen.citizenName}";
            docClassText.text = $"Certified Class: {citizen.certifiedClass}";
            docForgedText.text = citizen.isForged ? "DISCREPANCY DETECTED" : "Documents Valid";
            docForgedText.color = citizen.isForged ? Color.red : Color.green;

            // scanner
            scannerResultText.text = $"Scanner: {citizen.actualClass}";
            bool mismatch = citizen.certifiedClass != citizen.actualClass;
            scannerDisplay.color = mismatch ? Color.red : Color.green;

            UpdateDayAndTrust();
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
    }
}