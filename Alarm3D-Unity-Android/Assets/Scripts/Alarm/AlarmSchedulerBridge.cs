using System;
using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmSchedulerBridge : MonoBehaviour
    {
        [SerializeField]
        private AndroidAlarmSchedulerBridge androidScheduler;

        public bool Schedule(AlarmData alarm)
        {
            if (alarm == null ||
                string.IsNullOrWhiteSpace(alarm.id) ||
                !alarm.enabled)
            {
                return false;
            }

            if (!TryBuildNextFireTime(alarm, out DateTime fireTime))
                return false;

            if (androidScheduler == null)
            {
                androidScheduler =
                    FindFirstObjectByType<AndroidAlarmSchedulerBridge>();
            }

            if (androidScheduler == null)
            {
                Debug.LogError(
                    "AlarmSchedulerBridge: AndroidAlarmSchedulerBridge not found.");

                return false;
            }

            if (!androidScheduler.CanScheduleExactAlarms())
            {
                Debug.LogWarning(
                    "AlarmSchedulerBridge: exact alarm permission is not granted.");

                return false;
            }

            androidScheduler.Schedule(
                alarm.id,
                fireTime);

            return true;
        }

        public void Cancel(string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return;

            if (androidScheduler == null)
            {
                androidScheduler =
                    FindFirstObjectByType<AndroidAlarmSchedulerBridge>();
            }

            if (androidScheduler == null)
            {
                Debug.LogError(
                    "AlarmSchedulerBridge: AndroidAlarmSchedulerBridge not found.");

                return;
            }

            androidScheduler.Cancel(alarmId);
        }

        private static bool TryBuildNextFireTime(
            AlarmData alarm,
            out DateTime fireTime)
        {
            fireTime = default;

            if (alarm == null)
                return false;

            int hour = Mathf.Clamp(alarm.hour, 0, 23);
            int minute = Mathf.Clamp(alarm.minute, 0, 59);

            DateTime now = DateTime.Now;

            fireTime = new DateTime(
                now.Year,
                now.Month,
                now.Day,
                hour,
                minute,
                0,
                DateTimeKind.Local);

            if (fireTime <= now)
                fireTime = fireTime.AddDays(1);

            return true;
        }
    }
}
