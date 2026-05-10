using UnityEngine;
using System.Collections;
using DragonSeal.Data;

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

        public void GetCitizenDialogue(CitizenSO citizen, System.Action<string> onComplete)
        {
            StartCoroutine(SendRequest(citizen, onComplete));
        }

        private IEnumerator SendRequest(CitizenSO citizen, System.Action<string> onComplete)
        {
            // build prompt
            string systemPrompt = $@"You are a citizen in a dystopian world where humans have dragon powers.
                You are at a government Dragon Suppression Bureau checkpoint.
                Your name is {citizen.citizenName}, age {citizen.age}.
                Your personality: {citizen.openAIPersonality}.
                Your certified dragon class is {citizen.certifiedClass}.
                {(citizen.isForged ? "Your documents are forged. You are hiding your true power." : "Your documents are legitimate.")}
                Respond in 1-2 short sentences as this character. Stay in character. Be natural and human.";

            string userMessage = "The inspector is looking at you. What do you say?";

            Debug.Log($"System prompt built for {citizen.citizenName}");

            yield return null;
            onComplete?.Invoke(citizen.storyHint);
        }
    }
}
