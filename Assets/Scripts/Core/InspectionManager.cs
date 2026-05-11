using UnityEngine;
using System.Collections.Generic;
using DragonSeal.Data;

namespace DragonSeal.Core
{
    public class InspectionManager : MonoBehaviour
    {
        public static InspectionManager Instance { get; private set; }

        [Header("Day Setup")]
        [SerializeField] private List<CitizenSO> dayOneCitizens;
        [SerializeField] private List<CitizenSO> dayTwoCitizens;
        [SerializeField] private List<CitizenSO> dayThreeCitizens;

        private Queue<CitizenSO> _citizenQueue = new Queue<CitizenSO>();
        private CitizenSO _currentCitizen;

        public enum StampDecision { Approve, Reject }

        public System.Action<CitizenSO> OnCitizenArrived;
        public System.Action<StampDecision> OnDecisionMade;
        public System.Action OnDayComplete;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }

        private void Start()
        {
            LoadDayQueue();
            NextCitizen();
        }

        private void LoadDayQueue()
        {
            _citizenQueue.Clear();

            if (GameManager.Instance == null)
            {
                Debug.LogWarning("GameManager not found! Are you running from MainMenu scene?");
                return;
            }

            List<CitizenSO> todaysCitizens = GameManager.Instance.DayNumber switch
            {
                1 => dayOneCitizens,
                2 => dayTwoCitizens,
                3 => dayThreeCitizens,
                _ => dayOneCitizens
            };

            foreach (CitizenSO citizen in todaysCitizens)
                _citizenQueue.Enqueue(citizen);

            Debug.Log($"Day {GameManager.Instance.DayNumber} queue loaded with {_citizenQueue.Count} citizens.");
        }

        public void NextCitizen()
        {
            if (_citizenQueue.Count == 0)
            {
                Debug.Log("All citizens processed — day complete!");
                OnDayComplete?.Invoke();
                GameManager.Instance.EndDay();
                return;
            }

            _currentCitizen = _citizenQueue.Dequeue();
            OnCitizenArrived?.Invoke(_currentCitizen);
            Debug.Log($"Next citizen: {_currentCitizen.citizenName}");
        }

        public void MakeDecision(StampDecision decision)
        {
            if (_currentCitizen == null) return;

            if (_currentCitizen.citizenType == CitizenType.Story &&
                GameManager.Instance.DayNumber == 3)
            {
                GameManager.Instance.CurrentEnding = decision == StampDecision.Reject
                    ? GameManager.EndingType.ExposedTruth
                    : GameManager.EndingType.StayedSilent;
            }

            bool correct = IsDecisionCorrect(decision);

            switch (decision)
            {
                case StampDecision.Approve:
                    GameManager.Instance.ModifyTrust(correct ? +10 : -15);
                    break;
                case StampDecision.Reject:
                    GameManager.Instance.ModifyTrust(correct ? +10 : -15);
                    break;
            }

            OnDecisionMade?.Invoke(decision);
            Debug.Log($"Decision: {decision} | Correct: {correct} | Trust: {GameManager.Instance.TrustRating}");

            NextCitizen();
        }

        private bool IsDecisionCorrect(StampDecision decision)
        {
            if (_currentCitizen == null) return false;

            bool shouldReject =
                _currentCitizen.isForged ||
                !_currentCitizen.isRegistered ||
                !_currentCitizen.documentHasAppointment ||
                _currentCitizen.documentClass != _currentCitizen.trueClass ||
                _currentCitizen.documentName != _currentCitizen.citizenName ||
                _currentCitizen.documentAge != _currentCitizen.age ||
                _currentCitizen.documentGender != _currentCitizen.trueGender ||
                _currentCitizen.documentRegion != _currentCitizen.trueRegion ||
                _currentCitizen.documentExpiryDate != _currentCitizen.trueExpiryDate;

            return decision == StampDecision.Reject ? shouldReject : !shouldReject;
        }

        public CitizenSO GetCurrentCitizen() => _currentCitizen;
    }
}