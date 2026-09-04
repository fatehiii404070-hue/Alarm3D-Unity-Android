using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmSchedulingService : MonoBehaviour
    {
        [SerializeField]
        private AlarmManager alarmManager;

        [SerializeField]
        private AlarmSchedulerBridge schedulerBridge;

        private void Awake()
        {
            if (alarmManager == null)
            {
                alarmManager =
                    FindFirstObjectByType<AlarmManager>();
            }

            if (schedulerBridge == null)
            {
                schedulerBridge =
                    FindFirstObjectByType<AlarmSchedulerBridge>();
            }
        }

        private void OnEnable()
        {
            if (alarmManager != null)
            {
                alarmManager.AlarmsChanged += RescheduleAll;
            }
        }

        private void OnDisable()
        {
            if (alarmManager != null)
            {
                alarmManager.AlarmsChanged -= RescheduleAll;
            }
        }

        private void Start()
        {
            RescheduleAll();
        }

        public void RescheduleAll()
        {
            if (alarmManager == null || schedulerBridge == null)
            {
                Debug.LogError(
                    "AlarmSchedulingService: required dependencies are missing.");

                return;
            }

            foreach (AlarmData alarm in alarmManager.Alarms)
            {
                if (alarm == null ||
                    string.IsNullOrWhiteSpace(alarm.id))
                {
                    continue;
                }

                if (alarm.enabled)
                {
                    schedulerBridge.Schedule(alarm);
                }
                else
                {
                    schedulerBridge.Cancel(alarm.id);
                }
            }
        }

        public void ScheduleAlarm(AlarmData alarm)
        {
            if (alarm == null ||
                string.IsNullOrWhiteSpace(alarm.id))
            {
                return;
            }

            if (schedulerBridge == null)
            {
                Debug.LogError(
                    "AlarmSchedulingService: AlarmSchedulerBridge not found.");

                return;
            }

            if (alarm.enabled)
            {
                schedulerBridge.Schedule(alarm);
            }
            else
            {
                schedulerBridge.Cancel(alarm.id);
            }
        }

        public void CancelAlarm(string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return;

            schedulerBridge?.Cancel(alarmId);
        }
    }
}
