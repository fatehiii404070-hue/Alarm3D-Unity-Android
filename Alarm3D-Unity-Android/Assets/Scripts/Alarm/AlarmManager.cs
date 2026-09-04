using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alarm3D.Alarm
{
    public class AlarmManager : MonoBehaviour
    {
        public static AlarmManager Instance { get; private set; }

        [SerializeField]
        private List<AlarmData> alarms = new List<AlarmData>();

        public IReadOnlyList<AlarmData> Alarms => alarms;

        public event Action AlarmsChanged;

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

        public void AddAlarm(AlarmData alarm)
        {
            if (alarm == null)
                return;

            alarms.Add(alarm);
            AlarmsChanged?.Invoke();
        }

        public void RemoveAlarm(string id)
        {
            alarms.RemoveAll(alarm => alarm != null && alarm.id == id);
            AlarmsChanged?.Invoke();
        }

        public void SetAlarmEnabled(string id, bool enabled)
        {
            AlarmData alarm = alarms.Find(item => item != null && item.id == id);

            if (alarm == null)
                return;

            alarm.enabled = enabled;
            AlarmsChanged?.Invoke();
        }
    }
}
