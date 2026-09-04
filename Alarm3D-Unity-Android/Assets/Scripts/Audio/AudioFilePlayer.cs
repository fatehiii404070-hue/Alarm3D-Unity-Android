using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;

namespace Alarm3D.Audio
{
    [DisallowMultipleComponent]
    public sealed class AudioFilePlayer : MonoBehaviour
    {
        public static AudioFilePlayer Instance { get; private set; }

        [SerializeField]
        private AudioSource audioSource;

        private Coroutine loadingCoroutine;

        public bool IsPlaying =>
            audioSource != null && audioSource.isPlaying;

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
                audioSource = GetComponent<AudioSource>();

            if (audioSource == null)
                audioSource = gameObject.AddComponent<AudioSource>();

            audioSource.playOnAwake = false;
        }

        public void PlayFile(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            Stop();

            loadingCoroutine =
                StartCoroutine(LoadAndPlay(filePath));
        }

        public void Stop()
        {
            if (loadingCoroutine != null)
            {
                StopCoroutine(loadingCoroutine);
                loadingCoroutine = null;
            }

            if (audioSource != null)
            {
                audioSource.Stop();
                audioSource.clip = null;
            }
        }

        private IEnumerator LoadAndPlay(string filePath)
        {
            string url = new Uri(filePath).AbsoluteUri;

            using UnityWebRequest request =
                UnityWebRequestMultimedia.GetAudioClip(
                    url,
                    AudioType.WAV
                );

            yield return request.SendWebRequest();

            loadingCoroutine = null;

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError(
                    $"Audio loading failed: {request.error}");
                yield break;
            }

            AudioClip clip =
                DownloadHandlerAudioClip.GetContent(request);

            if (clip == null)
                yield break;

            audioSource.clip = clip;
            audioSource.Play();
        }

        private void OnDestroy()
        {
            Stop();
        }
    }
}
