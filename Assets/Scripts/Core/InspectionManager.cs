using UnityEngine;
using System.Collections.Generic;
using DragonSeal.Data;

namespace DragonSeal.Core
{
    public class InspectionManager : MonoBehaviour
    {
        public static InspectionManager Instance { get; private set; }

        // citizen queue
        [Header("Day Setup")]
        [SerializeField] private List<CitizenSO> dayOneCitizens;
        [SerializeField] private List<CitizenSO> dayTwoCitizens;
        [SerializeField] private List<CitizenSO> dayThreeCitizens;

        private Queue<CitizenSO> _citizenQueue = new Queue<CitizenSO>();
        private CitizenSO _currentCitizen;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
        }
    }
}
