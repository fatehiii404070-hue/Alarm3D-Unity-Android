using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmEditor : MonoBehaviour
    {
        [SerializeField]
        private AlarmManager alarmManager;

        private void Awake()
        {
            if (alarmManager == null)
            {
                alarmManager =
                    FindFirstObjectByType<AlarmManager>();
            }
        }

        public bool UpdateAlarm(
            string id,
            string title,
            int hour,
            int minute,
            bool enabled,
            string soundId)
        {
            if (alarmManager == null)
            {
                Debug.LogError(
                    "AlarmEditor: AlarmManager not found.");

                return false;
            }

            if (string.IsNullOrWhiteSpace(id))
                return false;

            AlarmData existingAlarm =
                alarmManager.GetAlarm(id);

            if (existingAlarm == null)
                return false;

            var updatedAlarm = new AlarmData
            {
                id = existingAlarm.id,
                title = title ?? string.Empty,
                hour = Mathf.Clamp(hour, 0, 23),
                minute = Mathf.Clamp(minute, 0, 59),
                enabled = enabled,
                soundId = soundId ?? string.Empty
            };

            return alarmManager.UpdateAlarm(updatedAlarm);
        }

        public bool SetEnabled(
            string id,
            bool enabled)
        {
            if (alarmManager == null)
            {
                Debug.LogError(
                    "AlarmEditor: AlarmManager not found.");

                return false;
            }

            return alarmManager.SetAlarmEnabled(
                id,
                enabled);
        }

        public bool SetSound(
            string id,
            string soundId)
        {
            if (alarmManager == null)
            {
                Debug.LogError(
                    "AlarmEditor: AlarmManager not found.");

                return false;
            }

            AlarmData alarm =
                alarmManager.GetAlarm(id);

            if (alarm == null)
                return false;

            alarm.soundId = soundId ?? string.Empty;

            return alarmManager.UpdateAlarm(alarm);
        }
    }
}
