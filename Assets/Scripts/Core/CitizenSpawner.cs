using UnityEngine;
using DragonSeal.Data;
using DragonSeal.NPC;

namespace DragonSeal.Core
{
    public class CitizenSpawner : MonoBehaviour
    {
        public static CitizenSpawner Instance { get; private set; }

        [Header("Prefabs")]
        [SerializeField] private GameObject regularCitizenPrefab;
        [SerializeField] private GameObject storyCitizenPrefab;
        [SerializeField] private GameObject fugitiveCitizenPrefab;

        private CitizenNPC _currentCitizenNPC;

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
            // listen
            InspectionManager.Instance.OnCitizenArrived += SpawnCitizen;
            InspectionManager.Instance.OnDecisionMade += DespawnCitizen;
        }

        private void OnDestroy()
        {
            if (InspectionManager.Instance != null)
            {
                InspectionManager.Instance.OnCitizenArrived -= SpawnCitizen;
                InspectionManager.Instance.OnDecisionMade -= DespawnCitizen;
            }
        }

    }
}
