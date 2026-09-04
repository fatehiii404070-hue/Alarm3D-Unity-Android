using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alarm3D.Audio
{
    [DisallowMultipleComponent]
    public sealed class RecordedVoiceLibrary : MonoBehaviour
    {
        public static RecordedVoiceLibrary Instance { get; private set; }

        [SerializeField]
        private List<RecordedVoiceAsset> voices =
            new List<RecordedVoiceAsset>();

        public IReadOnlyList<RecordedVoiceAsset> Voices => voices;

        public event Action LibraryChanged;

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

        public bool Add(RecordedVoiceAsset voice)
        {
            if (voice == null || !voice.IsValid())
                return false;

            if (Contains(voice.Id))
                return false;

            voices.Add(voice);
            LibraryChanged?.Invoke();

            return true;
        }

        public bool Remove(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            int removedCount = voices.RemoveAll(
                voice => voice != null && voice.Id == id);

            if (removedCount == 0)
                return false;

            LibraryChanged?.Invoke();
            return true;
        }

        public RecordedVoiceAsset Find(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            return voices.Find(
                voice => voice != null && voice.Id == id);
        }

        public bool Contains(string id)
        {
            return Find(id) != null;
        }

        public void Clear()
        {
            if (voices.Count == 0)
                return;

            voices.Clear();
            LibraryChanged?.Invoke();
        }
    }
}
