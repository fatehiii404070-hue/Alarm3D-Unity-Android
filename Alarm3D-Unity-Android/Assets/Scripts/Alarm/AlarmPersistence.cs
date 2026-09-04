using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmPersistence : MonoBehaviour
    {
        private const string AlarmsKey = "Alarm3D.Alarms";

        [SerializeField]
        private AlarmManager alarmManager;

        private void Awake()
        {
            if (alarmManager == null)
                alarmManager = GetComponent<AlarmManager>();
        }

        private void Start()
        {
            Load();
        }

        public void Save()
        {
            if (alarmManager == null)
                return;

            AlarmCollection collection = new AlarmCollection();

            foreach (AlarmData alarm in alarmManager.Alarms)
            {
                if (alarm == null)
                    continue;

                collection.alarms.Add(alarm);
            }

            string json = JsonUtility.ToJson(collection);

            PlayerPrefs.SetString(AlarmsKey, json);
            PlayerPrefs.Save();
        }

        public void Load()
        {
            if (alarmManager == null)
                return;

            if (!PlayerPrefs.HasKey(AlarmsKey))
                return;

            string json = PlayerPrefs.GetString(AlarmsKey);

            if (string.IsNullOrWhiteSpace(json))
                return;

            AlarmCollection collection;

            try
            {
                collection =
                    JsonUtility.FromJson<AlarmCollection>(json);
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"Failed to load alarms: {exception.Message}"
                );

                return;
            }

            if (collection?.alarms == null)
                return;

            foreach (AlarmData alarm in collection.alarms)
            {
                if (alarm == null ||
                    string.IsNullOrWhiteSpace(alarm.id))
                    continue;

                alarmManager.AddAlarm(alarm);
            }
        }

        [Serializable]
        private sealed class AlarmCollection
        {
            public List<AlarmData> alarms =
                new List<AlarmData>();
        }
    }
}
