using UnityEngine;
using UnityEngine.UI;
using TMPro;
using DragonSeal.Core;
using DragonSeal.Data;

namespace DragonSeal.UI
{
    public class DocumentCard : MonoBehaviour
    {
        [Header("Document Visual")]
        [SerializeField] private Image documentBackground;
        [SerializeField] private Image portraitImage;
        [SerializeField] private TextMeshProUGUI nameText;
        [SerializeField] private TextMeshProUGUI ageText;
        [SerializeField] private TextMeshProUGUI classText;
        [SerializeField] private TextMeshProUGUI genderText;
        [SerializeField] private TextMeshProUGUI regionText;
        [SerializeField] private TextMeshProUGUI expiryText;
        [SerializeField] private TextMeshProUGUI appointmentText;
        [SerializeField] private GameObject contentPanel;

        [Header("Stamp Mark (CitizenDocument only)")]
        [SerializeField] private Image stampMark;
        [SerializeField] private TextMeshProUGUI stampText;

        public bool IsViewed { get; private set; } = false;
        public bool IsStamped { get; private set; } = false;

        private void Awake()
        {
            contentPanel.SetActive(true);
            ClearStamp();
        }

        public void LoadAsDocument(CitizenSO citizen)
        {
            nameText.text = $"Name:         {citizen.documentName}";
            ageText.text = $"Age:            {citizen.documentAge}";
            classText.text = $"Class:          {citizen.documentClass}";
            genderText.text = $"Gender:       {citizen.documentGender}";
            regionText.text = $"Region:        {citizen.documentRegion}";
            expiryText.text = $"Expires:        {citizen.documentExpiryDate}";
            appointmentText.text = citizen.documentHasAppointment
                ? $"Appointment: {citizen.appointmentDate}"
                : "Appointment: NONE";

            // Load portrait
            if (portraitImage != null)
            {
                if (citizen.portrait != null)
                    portraitImage.sprite = citizen.portrait;
                else if (!string.IsNullOrEmpty(citizen.portraitKey))
                {
                    Sprite loaded = JsonImageLoader.Instance.GetPortrait(citizen.portraitKey);
                    if (loaded != null) portraitImage.sprite = loaded;
                }
            }

            IsViewed = true;
            IsStamped = false;
            ClearStamp();
        }

        public void LoadAsDatabase(CitizenSO citizen)
        {
            nameText.text = $"Name:         {citizen.citizenName}";
            ageText.text = $"Age:            {citizen.age}";
            classText.text = $"True Class:  {citizen.trueClass}";
            genderText.text = $"Gender:       {citizen.trueGender}";
            regionText.text = $"Region:        {citizen.trueRegion}";
            expiryText.text = $"Expires:        {citizen.trueExpiryDate}";
            appointmentText.text = citizen.isRegistered
                ? "Status:         REGISTERED"
                : "Status:         UNREGISTERED";

            IsViewed = true;
            IsStamped = false;
            ClearStamp();
        }

        // add stamp on doc
        public void ApplyStamp(InspectionManager.StampDecision decision)
        {
            if (stampMark == null || stampText == null) return;

            IsStamped = true;

            switch (decision)
            {
                case InspectionManager.StampDecision.Approve:
                    stampMark.color = new Color(0.1f, 0.6f, 0.1f, 0.85f);
                    stampText.text = "APPROVED";
                    break;
                case InspectionManager.StampDecision.Reject:
                    stampMark.color = new Color(0.6f, 0.1f, 0.1f, 0.85f);
                    stampText.text = "REJECTED";
                    break;
                case InspectionManager.StampDecision.Flag:
                    stampMark.color = new Color(0.6f, 0.5f, 0.1f, 0.85f);
                    stampText.text = "FLAGGED";
                    break;
            }

            stampText.color = Color.white;
        }

        private void ClearStamp()
        {
            if (stampMark == null) return;
            stampMark.color = new Color(0f, 0f, 0f, 0f);
            if (stampText != null) stampText.text = "";
        }

        public void ResetCard()
        {
            IsViewed = false;
            IsStamped = false;
            ClearStamp();
            contentPanel.SetActive(true);
        }
    }
}