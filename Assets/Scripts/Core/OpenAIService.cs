using UnityEngine;

namespace DragonSeal.Core
{
    public class OpenAIService : MonoBehaviour
    {
        public static OpenAIService Instance { get; private set; }

        [Header("OpenAI Settings")]
        [SerializeField] private string apiKey = "key";
        private const string API_URL = "https://api.anthropic.com/v1/messages";

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }
}
