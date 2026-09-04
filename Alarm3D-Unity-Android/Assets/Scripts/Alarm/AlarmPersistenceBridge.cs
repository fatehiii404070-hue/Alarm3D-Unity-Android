using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmPersistenceBridge : MonoBehaviour
    {
        [SerializeField]
        private AlarmManager alarmManager;

        [SerializeField]
        private AlarmPersistence persistence;

        private void Awake()
        {
            if (alarmManager == null)
                alarmManager = GetComponent<AlarmManager>();

            if (persistence == null)
                persistence = GetComponent<AlarmPersistence>();
        }

        private void OnEnable()
        {
            if (alarmManager != null)
                alarmManager.AlarmsChanged += Save;
        }

        private void OnDisable()
        {
            if (alarmManager != null)
                alarmManager.AlarmsChanged -= Save;
        }

        private void OnApplicationPause(bool paused)
        {
            if (paused)
                Save();
        }

        private void OnApplicationQuit()
        {
            Save();
        }

        private void Save()
        {
            if (persistence != null)
                persistence.Save();
        }
    }
}
