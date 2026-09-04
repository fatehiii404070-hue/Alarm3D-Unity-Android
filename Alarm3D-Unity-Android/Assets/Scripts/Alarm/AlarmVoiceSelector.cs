using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmVoiceSelector : MonoBehaviour
    {
        public bool SetVoiceForAlarm(string alarmId, string voiceId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return false;

            if (AlarmManager.Instance == null)
                return false;

            AlarmData alarm = FindAlarm(alarmId);

            if (alarm == null)
                return false;

            alarm.soundId = voiceId ?? string.Empty;

            AlarmManager.Instance.SetAlarmEnabled(
                alarmId,
                alarm.enabled
            );

            return true;
        }

        public string GetVoiceForAlarm(string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return string.Empty;

            AlarmData alarm = FindAlarm(alarmId);

            return alarm != null
                ? alarm.soundId
                : string.Empty;
        }

        private AlarmData FindAlarm(string alarmId)
        {
            if (AlarmManager.Instance == null)
                return null;

            foreach (AlarmData alarm in AlarmManager.Instance.Alarms)
            {
                if (alarm != null && alarm.id == alarmId)
                    return alarm;
            }

            return null;
        }
    }
}
