using System;
using UnityEngine;

namespace Alarm3D.Audio
{
    public class VoiceRecorder : MonoBehaviour
    {
        public bool IsRecording { get; private set; }

        public event Action<AudioClip> RecordingCompleted;

        private string microphoneDevice;

        public void StartRecording(int durationSeconds = 30)
        {
            if (IsRecording)
                return;

            if (Microphone.devices.Length == 0)
            {
                Debug.LogWarning("No microphone was found.");
                return;
            }

            microphoneDevice = Microphone.devices[0];
            IsRecording = true;

            AudioClip recording = Microphone.Start(
                microphoneDevice,
                false,
                durationSeconds,
                44100
            );

            if (recording == null)
            {
                IsRecording = false;
                return;
            }

            CancelInvoke(nameof(FinishRecording));
            Invoke(nameof(FinishRecording), durationSeconds);
        }

        public void StopRecording()
        {
            if (!IsRecording)
                return;

            FinishRecording();
        }

        private void FinishRecording()
        {
            if (!IsRecording)
                return;

            int samplePosition = Microphone.GetPosition(microphoneDevice);

            if (Microphone.IsRecording(microphoneDevice))
            {
                Microphone.End(microphoneDevice);
            }

            IsRecording = false;

            if (samplePosition <= 0)
                return;

            AudioClip recording = AudioClip.Create(
                "RecordedVoice",
                samplePosition,
                1,
                44100,
                false
            );

            RecordingCompleted?.Invoke(recording);
        }

        private void OnDestroy()
        {
            CancelInvoke();

            if (IsRecording && !string.IsNullOrEmpty(microphoneDevice))
            {
                Microphone.End(microphoneDevice);
                IsRecording = false;
            }
        }
    }
}
