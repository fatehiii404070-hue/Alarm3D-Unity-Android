using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alarm3D.Audio
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance { get; private set; }

        [SerializeField]
        private AudioSource audioSource;

        private readonly Dictionary<string, AudioClip> audioClips =
            new Dictionary<string, AudioClip>();

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            if (audioSource == null)
            {
                audioSource = gameObject.AddComponent<AudioSource>();
            }
        }

        public void RegisterClip(string id, AudioClip clip)
        {
            if (string.IsNullOrWhiteSpace(id) || clip == null)
                return;

            audioClips[id] = clip;
        }

        public void Play(string id)
        {
            if (!audioClips.TryGetValue(id, out AudioClip clip))
                return;

            audioSource.clip = clip;
            audioSource.Play();
        }

        public void Stop()
        {
            audioSource.Stop();
        }

        public bool HasClip(string id)
        {
            return !string.IsNullOrWhiteSpace(id) &&
                   audioClips.ContainsKey(id);
        }
    }
}
