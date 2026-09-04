using System.Collections.Generic;
using UnityEngine;

namespace Alarm3D.Audio
{
    public class VoiceLibrary : MonoBehaviour
    {
        public static VoiceLibrary Instance { get; private set; }

        [SerializeField]
        private List<AudioClipData> voices = new List<AudioClipData>();

        public IReadOnlyList<AudioClipData> Voices => voices;

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

        public void AddVoice(AudioClipData voice)
        {
            if (voice == null || string.IsNullOrWhiteSpace(voice.id))
                return;

            voices.RemoveAll(item => item != null && item.id == voice.id);
            voices.Add(voice);
        }

        public void RemoveVoice(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return;

            voices.RemoveAll(item => item != null && item.id == id);
        }

        public AudioClip GetVoice(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            AudioClipData voice =
                voices.Find(item => item != null && item.id == id);

            return voice != null ? voice.clip : null;
        }
    }
}
