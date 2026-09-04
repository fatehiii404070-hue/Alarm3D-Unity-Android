using System;
using UnityEngine;

namespace Alarm3D.Alarm
{
    public static class AlarmTimeCalculator
    {
        public static DateTime GetNextOccurrence(
            int hour,
            int minute)
        {
            DateTime now = DateTime.Now;

            DateTime next = new DateTime(
                now.Year,
                now.Month,
                now.Day,
                Mathf.Clamp(hour, 0, 23),
                Mathf.Clamp(minute, 0, 59),
                0,
                DateTimeKind.Local);

            if (next <= now)
            {
                next = next.AddDays(1);
            }

            return next;
        }
    }
}
