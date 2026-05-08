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
        }

        private void UpdateCitizenUI(CitizenSO citizen)
        {
        }

        private void OnDecisionMade(InspectionManager.StampDecision decision)
        {
        }

        private void UpdateDayAndTrust()
        {
        }
    }
}
