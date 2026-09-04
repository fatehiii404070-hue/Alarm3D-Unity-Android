using System;
using UnityEngine;

namespace Alarm3D.Audio
{
    [DisallowMultipleComponent]
    public sealed class RecordedVoiceService : MonoBehaviour
    {
        [SerializeField]
        private VoiceRecorder recorder;

        [SerializeField]
        private RecordedVoiceLibrary library;

        public bool IsRecording =>
            recorder != null && recorder.IsRecording;

        public event Action<RecordedVoiceAsset> VoiceSaved;

        private void Awake()
        {
            if (recorder == null)
                recorder = GetComponent<VoiceRecorder>();

            if (library == null)
                library = GetComponent<RecordedVoiceLibrary>();
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

        public void StartRecording()
        {
            if (recorder == null)
                return;

            recorder.StartRecording();
        }

        public void StopRecording()
        {
            if (recorder == null)
                return;

            recorder.StopRecording();
        }

        private void OnRecordingCompleted(AudioClip clip)
        {
            if (clip == null || library == null)
                return;

            string id = Guid.NewGuid().ToString("N");
            string displayName = $"Voice {DateTime.Now:yyyy-MM-dd HH-mm-ss}";
            string fileName = $"{id}.wav";

            RecordedVoiceAsset voice =
                new RecordedVoiceAsset(
                    id,
                    displayName,
                    fileName
                );

            if (!library.Add(voice))
                return;

            VoiceSaved?.Invoke(voice);
        }
    }
}
