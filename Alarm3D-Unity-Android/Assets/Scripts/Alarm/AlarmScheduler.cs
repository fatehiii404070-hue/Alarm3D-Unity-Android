using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmScheduler : MonoBehaviour
    {
        [SerializeField]
        private AlarmSchedulingService schedulingService;

        private void Awake()
        {
            if (schedulingService == null)
            {
                schedulingService =
                    FindFirstObjectByType<AlarmSchedulingService>();
            }
        }

        public void ScheduleAlarm(AlarmData alarm)
        {
            if (alarm == null)
                return;

            if (schedulingService == null)
            {
                Debug.LogError(
                    "AlarmScheduler: AlarmSchedulingService not found.");

                return;
            }

            schedulingService.ScheduleAlarm(alarm);
        }

        public void CancelAlarm(string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return;

            if (schedulingService == null)
            {
                Debug.LogError(
                    "AlarmScheduler: AlarmSchedulingService not found.");

                return;
            }

            schedulingService.CancelAlarm(alarmId);
        }

        public void RescheduleAll()
        {
            if (schedulingService == null)
            {
                Debug.LogError(
                    "AlarmScheduler: AlarmSchedulingService not found.");

                return;
            }

            schedulingService.RescheduleAll();
        }
    }
}
