using UnityEngine;

namespace Alarm3D.Audio
{
    [DisallowMultipleComponent]
    public sealed class VoicePlaybackController : MonoBehaviour
    {
        [SerializeField]
        private AudioFilePlayer audioFilePlayer;

        [SerializeField]
        private AudioFileStorage audioFileStorage;

        [SerializeField]
        private RecordedVoiceLibrary voiceLibrary;

        private void Awake()
        {
            if (audioFilePlayer == null)
                audioFilePlayer = GetComponent<AudioFilePlayer>();

            if (audioFileStorage == null)
                audioFileStorage = GetComponent<AudioFileStorage>();

            if (voiceLibrary == null)
                voiceLibrary = GetComponent<RecordedVoiceLibrary>();
        }

        public void PlayVoice(string voiceId)
        {
            if (string.IsNullOrWhiteSpace(voiceId))
                return;

            if (audioFilePlayer == null ||
                audioFileStorage == null ||
                voiceLibrary == null)
                return;

            RecordedVoiceAsset voice =
                voiceLibrary.Find(voiceId);

            if (voice == null || !voice.IsValid())
                return;

            string filePath;

            try
            {
                filePath =
                    audioFileStorage.SaveFilePath(
                        voice.FileName
                    );
            }
            catch
            {
                return;
            }

            if (!audioFileStorage.FileExists(voice.FileName))
            {
                Debug.LogWarning(
                    $"Voice file not found: {voice.FileName}"
                );

                return;
            }

            audioFilePlayer.PlayFile(filePath);
        }

        public void StopVoice()
        {
            if (audioFilePlayer == null)
                return;

            audioFilePlayer.Stop();
        }
    }
}
