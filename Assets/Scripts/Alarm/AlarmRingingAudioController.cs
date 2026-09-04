using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmRingingAudioController : MonoBehaviour
    {
        [SerializeField]
        private AlarmRuntimeState runtimeState;

        [SerializeField]
        private AudioSource audioSource;

        [SerializeField]
        private AudioClip defaultAlarmClip;

        private void Awake()
        {
            if (runtimeState == null)
            {
                runtimeState =
                    FindFirstObjectByType<AlarmRuntimeState>();
            }

            if (audioSource == null)
            {
                audioSource =
                    GetComponent<AudioSource>();
            }

            if (audioSource == null)
            {
                audioSource =
                    gameObject.AddComponent<AudioSource>();
            }

            audioSource.playOnAwake = false;
            audioSource.loop = true;
        }

        private void OnEnable()
        {
            if (runtimeState != null)
            {
                runtimeState.RingingStateChanged +=
                    HandleRingingStateChanged;
            }
        }

        private void OnDisable()
        {
            if (runtimeState != null)
            {
                runtimeState.RingingStateChanged -=
                    HandleRingingStateChanged;
            }

            StopAudio();
        }

        private void HandleRingingStateChanged(
            bool isRinging)
        {
            if (isRinging)
            {
                StartAudio();
            }
            else
            {
                StopAudio();
            }
        }

        public void StartAudio()
        {
            if (audioSource == null ||
                defaultAlarmClip == null)
            {
                return;
            }

            if (audioSource.isPlaying)
            {
                return;
            }

            audioSource.clip = defaultAlarmClip;
            audioSource.loop = true;
            audioSource.Play();
        }

        public void StopAudio()
        {
            if (audioSource == null)
            {
                return;
            }

            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }

            audioSource.clip = null;
        }
    }
}
