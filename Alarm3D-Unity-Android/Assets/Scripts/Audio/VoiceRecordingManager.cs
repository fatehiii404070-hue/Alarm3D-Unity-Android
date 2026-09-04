using System;
using UnityEngine;

namespace Alarm3D.Audio
{
    [DisallowMultipleComponent]
    public sealed class VoiceRecordingManager : MonoBehaviour
    {
        public static VoiceRecordingManager Instance { get; private set; }

        [SerializeField]
        private VoiceRecorder recorder;

        [SerializeField]
        private RecordedVoiceLibrary library;

        [SerializeField]
        private AudioFileStorage storage;

        public bool IsRecording =>
            recorder != null && recorder.IsRecording;

        public event Action<RecordedVoiceAsset> VoiceSaved;
        public event Action RecordingStarted;
        public event Action RecordingStopped;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            ResolveDependencies();
        }

        private void OnEnable()
        {
            if (recorder != null)
                recorder.RecordingCompleted += OnRecordingCompleted;
        }

        private void OnDisable()
        {
            if (recorder != null)
                recorder.RecordingCompleted -= OnRecordingCompleted;
        }

        public bool StartRecording()
        {
            if (recorder == null || recorder.IsRecording)
                return false;

            recorder.StartRecording();
            RecordingStarted?.Invoke();

            return recorder.IsRecording;
        }

        public void StopRecording()
        {
            if (recorder == null || !recorder.IsRecording)
                return;

            recorder.StopRecording();
            RecordingStopped?.Invoke();
        }

        private void OnRecordingCompleted(AudioClip clip)
        {
            if (clip == null || library == null || storage == null)
                return;

            string id = Guid.NewGuid().ToString("N");
            string fileName = $"{id}.wav";
            string displayName =
                $"Voice {DateTime.Now:yyyy-MM-dd HH-mm-ss}";

            string filePath;

            try
            {
                filePath = storage.SaveFilePath(fileName);
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"Unable to create voice file path: {exception.Message}");

                return;
            }

            if (!WavEncoder.Save(clip, filePath))
                return;

            RecordedVoiceAsset voice =
                new RecordedVoiceAsset(
                    id,
                    displayName,
                    fileName
                );

            if (!library.Add(voice))
            {
                storage.DeleteFile(fileName);
                return;
            }

            VoiceSaved?.Invoke(voice);

            Destroy(clip);
        }

        private void ResolveDependencies()
        {
            if (recorder == null)
                recorder = GetComponent<VoiceRecorder>();

            if (library == null)
                library = GetComponent<RecordedVoiceLibrary>();

            if (storage == null)
                storage = GetComponent<AudioFileStorage>();
        }
    }
}
