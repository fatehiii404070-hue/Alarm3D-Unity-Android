using System;
using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmSchedulerBridge : MonoBehaviour
    {
        [SerializeField]
        private AndroidAlarmSchedulerBridge androidScheduler;

        public void Schedule(AlarmData alarm)
        {
            if (alarm == null ||
                string.IsNullOrWhiteSpace(alarm.id) ||
                !alarm.enabled)
            {
                return;
            }

            DateTime now = DateTime.Now;

            DateTime fireTime = new DateTime(
                now.Year,
                now.Month,
                now.Day,
                Mathf.Clamp(alarm.hour, 0, 23),
                Mathf.Clamp(alarm.minute, 0, 59),
                0,
                DateTimeKind.Local);

            if (fireTime <= now)
                fireTime = fireTime.AddDays(1);

            if (androidScheduler == null)
                androidScheduler =
                    FindFirstObjectByType<AndroidAlarmSchedulerBridge>();

            if (androidScheduler == null)
            {
                Debug.LogError(
                    "AlarmSchedulerBridge: AndroidAlarmSchedulerBridge not found.");
                return;
            }

            androidScheduler.Schedule(
                alarm.id,
                fireTime);
        }

        public void Cancel(string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return;

            if (androidScheduler == null)
                androidScheduler =
                    FindFirstObjectByType<AndroidAlarmSchedulerBridge>();

            if (androidScheduler == null)
            {
                Debug.LogError(
                    "AlarmSchedulerBridge: AndroidAlarmSchedulerBridge not found.");
                return;
            }

            androidScheduler.Cancel(alarmId);
        }
    }
}
