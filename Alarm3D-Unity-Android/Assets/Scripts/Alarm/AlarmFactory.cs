using UnityEngine;

namespace Alarm3D.Alarm
{
    public static class AlarmFactory
    {
        public static AlarmData Create(
            string title,
            int hour,
            int minute,
            bool enabled = true,
            string soundId = "")
        {
            return new AlarmData
            {
                id = AlarmIdGenerator.Create(),
                title = string.IsNullOrWhiteSpace(title)
                    ? "هشدار جدید"
                    : title.Trim(),
                hour = Mathf.Clamp(hour, 0, 23),
                minute = Mathf.Clamp(minute, 0, 59),
                enabled = enabled,
                soundId = soundId ?? string.Empty
            };
        }
    }
}
