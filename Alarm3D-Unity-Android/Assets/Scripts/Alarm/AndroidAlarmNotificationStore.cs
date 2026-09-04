using System;
using System.Collections.Generic;
using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AndroidAlarmNotificationStore : MonoBehaviour
    {
        private const string StorageKey = "Alarm3D.AndroidNotificationIds";

        [Serializable]
        private sealed class Entry
        {
            public string alarmId;
            public int notificationId;
        }

        [Serializable]
        private sealed class EntryList
        {
            public List<Entry> entries = new List<Entry>();
        }

        private readonly Dictionary<string, int> notificationIds =
            new Dictionary<string, int>();

        private void Awake()
        {
            Load();
        }

        public void SetNotificationId(string alarmId, int notificationId)
        {
            if (string.IsNullOrWhiteSpace(alarmId) || notificationId < 0)
                return;

            notificationIds[alarmId] = notificationId;
            Save();
        }

        public bool TryGetNotificationId(
            string alarmId,
            out int notificationId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
            {
                notificationId = -1;
                return false;
            }

            return notificationIds.TryGetValue(
                alarmId,
                out notificationId);
        }

        public void RemoveNotificationId(string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return;

            if (notificationIds.Remove(alarmId))
                Save();
        }

        public void Clear()
        {
            notificationIds.Clear();
            Save();
        }

        private void Save()
        {
            var data = new EntryList();

            foreach (var pair in notificationIds)
            {
                data.entries.Add(new Entry
                {
                    alarmId = pair.Key,
                    notificationId = pair.Value
                });
            }

            PlayerPrefs.SetString(
                StorageKey,
                JsonUtility.ToJson(data));

            PlayerPrefs.Save();
        }

        private void Load()
        {
            notificationIds.Clear();

            if (!PlayerPrefs.HasKey(StorageKey))
                return;

            string json = PlayerPrefs.GetString(StorageKey);

            if (string.IsNullOrWhiteSpace(json))
                return;

            try
            {
                var data = JsonUtility.FromJson<EntryList>(json);

                if (data?.entries == null)
                    return;

                foreach (var entry in data.entries)
                {
                    if (entry == null ||
                        string.IsNullOrWhiteSpace(entry.alarmId) ||
                        entry.notificationId < 0)
                    {
                        continue;
                    }

                    notificationIds[entry.alarmId] =
                        entry.notificationId;
                }
            }
            catch (Exception exception)
            {
                Debug.LogError(
                    $"Failed to load Android notification IDs: {exception}");
            }
        }
    }
}
