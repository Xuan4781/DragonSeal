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

        // spawn citizen
        private void SpawnCitizen(CitizenSO citizenData)
        {
            if (_currentCitizenNPC != null)
                Destroy(_currentCitizenNPC.gameObject);

            GameObject prefabToSpawn = citizenData.citizenType switch
            {
                CitizenType.Regular => regularCitizenPrefab,
                CitizenType.Story => storyCitizenPrefab,
                CitizenType.Fugitive => fugitiveCitizenPrefab,
                _ => regularCitizenPrefab
            };

            if (prefabToSpawn == null)
            {
                Debug.LogWarning("Citizen prefab not assigned in CitizenSpawner!");
                return;
            }

            GameObject citizenGO = Instantiate(prefabToSpawn);
            _currentCitizenNPC = citizenGO.GetComponent<CitizenNPC>();
            _currentCitizenNPC.Initialize(citizenData);
        }

        // citizen walk out after the decision
        private void DespawnCitizen(InspectionManager.StampDecision decision)
        {
            if (_currentCitizenNPC != null)
                _currentCitizenNPC.WalkOut();
        }

    }
}
