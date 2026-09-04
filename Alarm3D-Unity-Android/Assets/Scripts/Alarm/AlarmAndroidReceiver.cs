using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmAndroidReceiver : MonoBehaviour
    {
        [SerializeField]
        private AlarmRingingManager ringingManager;

        private void Awake()
        {
            if (ringingManager == null)
            {
                ringingManager =
                    FindFirstObjectByType<AlarmRingingManager>();
            }
        }

        public void OnAlarmTriggered(
            string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
            {
                Debug.LogWarning(
                    "AlarmAndroidReceiver: empty alarm id.");

                return;
            }

            if (ringingManager == null)
            {
                Debug.LogError(
                    "AlarmAndroidReceiver: " +
                    "AlarmRingingManager not found.");

                return;
            }

            ringingManager.StartAlarm(
                alarmId);
        }
    }
}
