using UnityEngine;
using System.Collections.Generic;

namespace DragonSeal.Core
{
    [System.Serializable]
    public class PortraitEntry
    {
        public string key;
        public string path;
    }

    [System.Serializable]
    public class PortraitManifest
    {
        public List<PortraitEntry> portraits;
    }

    public class JsonImageLoader : MonoBehaviour
    {
        public static JsonImageLoader Instance { get; private set; }

        private Dictionary<string, string> _portraitPaths
            = new Dictionary<string, string>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            DontDestroyOnLoad(gameObject);
            LoadManifest();
        }

        private void LoadManifest()
        {
            TextAsset jsonFile = Resources.Load<TextAsset>("Portraits/portrait_manifest");

            if (jsonFile == null)
            {
                Debug.LogWarning("portrait_manifest.json not found in Resources/Portraits/");
                return;
            }

            PortraitManifest manifest = JsonUtility.FromJson<PortraitManifest>(jsonFile.text);

            foreach (PortraitEntry entry in manifest.portraits)
                _portraitPaths[entry.key] = entry.path;

            Debug.Log($"Portrait manifest loaded — {_portraitPaths.Count} portraits registered.");
        }

        public Sprite GetPortrait(string key)
        {
            if (!_portraitPaths.ContainsKey(key))
            {
                Debug.LogWarning($"Portrait key '{key}' not found in manifest.");
                return null;
            }

            Sprite sprite = Resources.Load<Sprite>(_portraitPaths[key]);

            if (sprite == null)
                Debug.LogWarning($"Sprite not found at path: {_portraitPaths[key]}");

            return sprite;
        }
        
    }
}
