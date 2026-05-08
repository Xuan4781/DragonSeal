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

        private void Start()
        {
            approveButton.onClick.AddListener(() =>
                InspectionManager.Instance.MakeDecision(InspectionManager.StampDecision.Approve));
            rejectButton.onClick.AddListener(() =>
                InspectionManager.Instance.MakeDecision(InspectionManager.StampDecision.Reject));
            flagButton.onClick.AddListener(() =>
                InspectionManager.Instance.MakeDecision(InspectionManager.StampDecision.Flag));

            talkButton.onClick.AddListener(OnTalkClicked);

            InspectionManager.Instance.OnCitizenArrived += UpdateCitizenUI;
            InspectionManager.Instance.OnDecisionMade += OnDecisionMade;

            UpdateDayAndTrust();
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

            // use OpenAI in a bit later
            citizenDialogueText.text = string.IsNullOrEmpty(current.storyHint)
                ? "I just want to get this over with...duh"
                : current.storyHint;
        }

        private void UpdateCitizenUI(CitizenSO citizen)
        {
            citizenNameText.text = citizen.citizenName;
            citizenDialogueText.text = "...";

            // Show port
            if (citizen.portrait != null)
                citizenPortrait.sprite = citizen.portrait;

            // doc 
            docNameText.text = $"Name: {citizen.citizenName}";
            docClassText.text = $"Certified Class: {citizen.certifiedClass}";
            docForgedText.text = citizen.isForged ? " DISCREPANCY DETECTED" : " Documents Valid";
            docForgedText.color = citizen.isForged ? Color.red : Color.green;

            // scanner reading
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
