using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmRingingHapticsController : MonoBehaviour
    {
        [SerializeField]
        private AlarmRuntimeState runtimeState;

        [SerializeField]
        private bool enableVibration = true;

        [SerializeField]
        private float vibrationInterval = 1f;

        private float nextVibrationTime;

        private void Awake()
        {
            if (runtimeState == null)
            {
                runtimeState =
                    FindFirstObjectByType<AlarmRuntimeState>();
            }
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
        }

        private void Update()
        {
            if (runtimeState == null ||
                !runtimeState.IsRinging ||
                !enableVibration)
            {
                return;
            }

            if (Time.unscaledTime < nextVibrationTime)
            {
                return;
            }

            TriggerVibration();

            nextVibrationTime =
                Time.unscaledTime +
                Mathf.Max(0.1f, vibrationInterval);
        }

        private void HandleRingingStateChanged(
            bool isRinging)
        {
            if (isRinging)
            {
                nextVibrationTime =
                    Time.unscaledTime;
            }
        }

        private static void TriggerVibration()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            Handheld.Vibrate();
#endif
        }
    }
}
