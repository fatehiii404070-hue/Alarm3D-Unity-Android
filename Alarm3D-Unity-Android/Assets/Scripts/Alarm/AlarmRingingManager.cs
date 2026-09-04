using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmRingingManager : MonoBehaviour
    {
        [SerializeField]
        private AlarmRuntimeState runtimeState;

        [SerializeField]
        private AlarmRingingAudioController audioController;

        [SerializeField]
        private AlarmRingingVisualController visualController;

        [SerializeField]
        private AlarmRingingHapticsController hapticsController;

        private void Awake()
        {
            if (runtimeState == null)
            {
                runtimeState =
                    FindFirstObjectByType<AlarmRuntimeState>();
            }

            if (audioController == null)
            {
                audioController =
                    FindFirstObjectByType<AlarmRingingAudioController>();
            }

            if (visualController == null)
            {
                visualController =
                    FindFirstObjectByType<AlarmRingingVisualController>();
            }

            if (hapticsController == null)
            {
                hapticsController =
                    FindFirstObjectByType<AlarmRingingHapticsController>();
            }
        }

        public void StartAlarm(
            string alarmId)
        {
            if (runtimeState == null)
            {
                Debug.LogError(
                    "AlarmRingingManager: " +
                    "AlarmRuntimeState not found.");

                return;
            }

            runtimeState.SetRinging(
                alarmId,
                true);
        }

        public void StopAlarm()
        {
            if (runtimeState == null)
            {
                return;
            }

            runtimeState.StopRinging();
        }

        public bool IsRinging()
        {
            return runtimeState != null &&
                   runtimeState.IsRinging;
        }

        public string GetActiveAlarmId()
        {
            if (runtimeState == null)
            {
                return string.Empty;
            }

            return runtimeState.ActiveAlarmId;
        }
    }
}
