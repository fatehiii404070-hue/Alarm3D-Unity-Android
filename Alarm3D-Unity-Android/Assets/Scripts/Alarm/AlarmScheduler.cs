using System;
using UnityEngine;

namespace Alarm3D.Alarm
{
    public class AlarmScheduler : MonoBehaviour
    {
        public static AlarmScheduler Instance { get; private set; }

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        private void Update()
        {
            CheckAlarms();
        }

        private void CheckAlarms()
        {
            if (AlarmManager.Instance == null)
                return;

            DateTime now = DateTime.Now;

            foreach (AlarmData alarm in AlarmManager.Instance.Alarms)
            {
                if (alarm == null || !alarm.enabled)
                    continue;

                if (alarm.hour == now.Hour &&
                    alarm.minute == now.Minute &&
                    now.Second == 0)
                {
                    TriggerAlarm(alarm);
                }
            }
        }

        private void TriggerAlarm(AlarmData alarm)
        {
            Debug.Log($"Alarm triggered: {alarm.title}");

            if (AudioManager.Instance != null &&
                !string.IsNullOrWhiteSpace(alarm.soundId))
            {
                AudioManager.Instance.Play(alarm.soundId);
            }
        }
    }
}
