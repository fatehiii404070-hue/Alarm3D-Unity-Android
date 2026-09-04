using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmManager : MonoBehaviour
    {
        public static AlarmManager Instance { get; private set; }

        [SerializeField]
        private List<AlarmData> alarms =
            new List<AlarmData>();

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

        public bool AddAlarm(AlarmData alarm)
        {
            if (alarm == null ||
                string.IsNullOrWhiteSpace(alarm.id))
            {
                return false;
            }

            if (alarms.Exists(
                item => item != null &&
                        item.id == alarm.id))
            {
                return false;
            }

            alarm.hour =
                Mathf.Clamp(alarm.hour, 0, 23);

            alarm.minute =
                Mathf.Clamp(alarm.minute, 0, 59);

            alarms.Add(alarm);

            AlarmsChanged?.Invoke();

            return true;
        }

        public bool UpdateAlarm(AlarmData updatedAlarm)
        {
            if (updatedAlarm == null ||
                string.IsNullOrWhiteSpace(updatedAlarm.id))
            {
                return false;
            }

            int index = alarms.FindIndex(
                item => item != null &&
                        item.id == updatedAlarm.id);

            if (index < 0)
                return false;

            updatedAlarm.hour =
                Mathf.Clamp(updatedAlarm.hour, 0, 23);

            updatedAlarm.minute =
                Mathf.Clamp(updatedAlarm.minute, 0, 59);

            alarms[index] = updatedAlarm;

            AlarmsChanged?.Invoke();

            return true;
        }

        public bool RemoveAlarm(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            int removedCount = alarms.RemoveAll(
                alarm => alarm != null &&
                         alarm.id == id);

            if (removedCount <= 0)
                return false;

            AlarmsChanged?.Invoke();

            return true;
        }

        public bool SetAlarmEnabled(
            string id,
            bool enabled)
        {
            if (string.IsNullOrWhiteSpace(id))
                return false;

            AlarmData alarm = alarms.Find(
                item => item != null &&
                        item.id == id);

            if (alarm == null)
                return false;

            if (alarm.enabled == enabled)
                return true;

            alarm.enabled = enabled;

            AlarmsChanged?.Invoke();

            return true;
        }

        public AlarmData GetAlarm(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
                return null;

            return alarms.Find(
                item => item != null &&
                        item.id == id);
        }

        public void ClearAlarms()
        {
            if (alarms.Count == 0)
                return;

            alarms.Clear();

            AlarmsChanged?.Invoke();
        }

        public void BeginBulkUpdate()
        {
        }

        public void EndBulkUpdate()
        {
            AlarmsChanged?.Invoke();
        }
    }
}
