using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmRingingController : MonoBehaviour
    {
        [SerializeField]
        private AlarmRuntimeState runtimeState;

        [SerializeField]
        private AlarmStopController stopController;

        private void Awake()
        {
            if (runtimeState == null)
            {
                runtimeState =
                    FindFirstObjectByType<AlarmRuntimeState>();
            }

            if (stopController == null)
            {
                stopController =
                    FindFirstObjectByType<AlarmStopController>();
            }
        }

        public void StartRinging(string alarmId)
        {
            if (runtimeState == null)
            {
                Debug.LogError(
                    "AlarmRingingController: " +
                    "AlarmRuntimeState not found.");

                return;
            }

            runtimeState.SetRinging(
                alarmId,
                true);
        }

        public void StopRinging()
        {
            stopController?.StopActiveAlarm();

            runtimeState?.StopRinging();
        }
    }
}
