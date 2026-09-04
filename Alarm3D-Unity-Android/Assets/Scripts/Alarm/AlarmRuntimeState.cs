using System;
using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmRuntimeState : MonoBehaviour
    {
        public static AlarmRuntimeState Instance { get; private set; }

        public bool IsRinging { get; private set; }

        public string ActiveAlarmId { get; private set; }

        public event Action<bool> RingingStateChanged;

        private void Awake()
        {
            if (Instance != null &&
                Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;

            DontDestroyOnLoad(gameObject);
        }

        public void SetRinging(
            string alarmId,
            bool ringing)
        {
            IsRinging = ringing;

            ActiveAlarmId =
                ringing
                    ? alarmId ?? string.Empty
                    : string.Empty;

            RingingStateChanged?.Invoke(
                IsRinging);
        }

        public void StopRinging()
        {
            SetRinging(
                string.Empty,
                false);
        }
    }
}
