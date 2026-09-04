using System.Collections.Generic;

namespace Alarm3D.Alarm
{
    public static class AlarmCollectionValidator
    {
        public static bool IsValid(
            IReadOnlyList<AlarmData> alarms)
        {
            if (alarms == null)
                return false;

            var ids = new HashSet<string>();

            foreach (AlarmData alarm in alarms)
            {
                if (alarm == null ||
                    string.IsNullOrWhiteSpace(alarm.id))
                {
                    return false;
                }

                if (!AlarmTimeValidator.IsValid(
                        alarm.hour,
                        alarm.minute))
                {
                    return false;
                }

                if (!ids.Add(alarm.id))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
