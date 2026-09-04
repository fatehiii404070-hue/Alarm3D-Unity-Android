using System;
using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmEditor : MonoBehaviour
    {
        public bool CreateAlarm(
            string title,
            int hour,
            int minute,
            bool enabled,
            string soundId,
            out string alarmId)
        {
            alarmId = string.Empty;

            title = AlarmValidator.NormalizeTitle(title);

            if (!AlarmValidator.IsValidTime(hour, minute))
                return false;

            AlarmData alarm = new AlarmData
            {
                id = Guid.NewGuid().ToString("N"),
                title = title,
                hour = hour,
                minute = minute,
                enabled = enabled,
                soundId = soundId ?? string.Empty
            };

            if (!AlarmValidator.IsValid(alarm))
                return false;

            if (AlarmManager.Instance == null)
                return false;

            AlarmManager.Instance.AddAlarm(alarm);

            alarmId = alarm.id;
            return true;
        }

        public bool UpdateAlarm(
            string alarmId,
            string title,
            int hour,
            int minute,
            bool enabled,
            string soundId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return false;

            if (!AlarmValidator.IsValidTime(hour, minute))
                return false;

            if (AlarmManager.Instance == null)
                return false;

            AlarmData alarm = FindAlarm(alarmId);

            if (alarm == null)
                return false;

            alarm.title =
                AlarmValidator.NormalizeTitle(title);

            alarm.hour = hour;
            alarm.minute = minute;
            alarm.enabled = enabled;
            alarm.soundId = soundId ?? string.Empty;

            return AlarmValidator.IsValid(alarm);
        }

        public bool DeleteAlarm(string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return false;

            if (AlarmManager.Instance == null)
                return false;

            AlarmManager.Instance.RemoveAlarm(alarmId);
            return true;
        }

        private AlarmData FindAlarm(string alarmId)
        {
            foreach (AlarmData alarm in AlarmManager.Instance.Alarms)
            {
                if (alarm != null && alarm.id == alarmId)
                    return alarm;
            }

            return null;
        }
    }
}
