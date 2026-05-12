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
            nameText.text = citizen.documentName;
            ageText.text = citizen.documentAge.ToString();
            classText.text = citizen.documentClass.ToString();
            genderText.text = citizen.documentGender.ToString();
            regionText.text = citizen.documentRegion.ToString();
            expiryText.text = citizen.documentExpiryDate;
            appointmentText.text = citizen.documentHasAppointment
                ? citizen.appointmentDate
                : "NONE";

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
            nameText.text = citizen.citizenName;
            ageText.text = citizen.age.ToString();
            classText.text = citizen.trueClass.ToString();
            genderText.text = citizen.trueGender.ToString();
            regionText.text = citizen.trueRegion.ToString();
            expiryText.text = citizen.trueExpiryDate;
            appointmentText.text = citizen.isRegistered ? "REGISTERED" : "UNREGISTERED";

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