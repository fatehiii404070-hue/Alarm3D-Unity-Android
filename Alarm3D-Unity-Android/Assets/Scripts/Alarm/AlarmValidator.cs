using System;

namespace Alarm3D.Alarm
{
    public static class AlarmValidator
    {
        public static bool IsValid(AlarmData alarm)
        {
            if (alarm == null)
                return false;

            if (string.IsNullOrWhiteSpace(alarm.id))
                return false;

            if (string.IsNullOrWhiteSpace(alarm.title))
                return false;

            if (alarm.hour < 0 || alarm.hour > 23)
                return false;

            if (alarm.minute < 0 || alarm.minute > 59)
                return false;

            return true;
        }

        public static bool IsValidTime(int hour, int minute)
        {
            return hour >= 0 &&
                   hour <= 23 &&
                   minute >= 0 &&
                   minute <= 59;
        }

        public static string NormalizeTitle(string title)
        {
            return string.IsNullOrWhiteSpace(title)
                ? "Alarm"
                : title.Trim();
        }
    }
}
