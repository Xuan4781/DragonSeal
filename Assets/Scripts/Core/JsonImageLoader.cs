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
        }
    }
}
