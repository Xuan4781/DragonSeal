using UnityEngine;
using UnityEngine.Networking;
using System.Collections;
using System.Text;
using DragonSeal.Data;

namespace DragonSeal.Core
{
    public class OpenAIService : MonoBehaviour
    {
        public static OpenAIService Instance { get; private set; }

        [Header("OpenAI Settings")]
        [SerializeField] private string apiKey = "key";
        private const string API_URL = "https://api.openai.com/v1/chat/completions";

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

            string jsonBody = $@"{{
                ""model"": ""gpt-4o-mini"",
                ""max_tokens"": 150,
                ""messages"": [
                    {{""role"": ""system"", ""content"": ""{EscapeJson(systemPrompt)}""}},
                    {{""role"": ""user"", ""content"": ""{EscapeJson(userMessage)}""}}
                ]
            }}";

            byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonBody);

            using UnityWebRequest request = new UnityWebRequest(API_URL, "POST");
            request.uploadHandler = new UploadHandlerRaw(bodyRaw);
            request.downloadHandler = new DownloadHandlerBuffer();
            request.SetRequestHeader("Content-Type", "application/json");
            request.SetRequestHeader("Authorization", $"Bearer {apiKey}");

            yield return request.SendWebRequest();

            if (request.result == UnityWebRequest.Result.Success)
            {
                string response = request.downloadHandler.text;
                string dialogue = ParseResponse(response);
                onComplete?.Invoke(dialogue);
            }
            else
            {
                Debug.LogWarning($"OpenAI request failed: {request.error}");
                onComplete?.Invoke(citizen.storyHint);
            }
        }

        // parse
        private string ParseResponse(string json)
        {
            try
            {
                int choicesIndex = json.IndexOf("\"content\":");
                if (choicesIndex == -1) return "...";

                int start = json.IndexOf("\"", choicesIndex + 10) + 1;
                int end = json.IndexOf("\"", start);
                return json.Substring(start, end - start);
            }
            catch
            {
                return "...";
            }
        }

        private string EscapeJson(string text)
        {
            return text
                .Replace("\\", "\\\\")
                .Replace("\"", "\\\"")
                .Replace("\n", "\\n")
                .Replace("\r", "\\r");
        }
    }
}