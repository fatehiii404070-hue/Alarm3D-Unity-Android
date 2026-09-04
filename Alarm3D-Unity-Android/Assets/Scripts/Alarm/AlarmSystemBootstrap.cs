using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmSystemBootstrap : MonoBehaviour
    {
        [SerializeField]
        private AlarmManager alarmManager;

        [SerializeField]
        private AlarmPersistence alarmPersistence;

        [SerializeField]
        private AlarmSchedulingService schedulingService;

        private void Awake()
        {
            alarmManager ??=
                FindFirstObjectByType<AlarmManager>();

            alarmPersistence ??=
                FindFirstObjectByType<AlarmPersistence>();

            schedulingService ??=
                FindFirstObjectByType<AlarmSchedulingService>();
        }

        private void Start()
        {
            if (alarmManager == null)
            {
                Debug.LogError(
                    "AlarmSystemBootstrap: AlarmManager not found.");
                return;
            }

            if (alarmPersistence == null)
            {
                Debug.LogError(
                    "AlarmSystemBootstrap: AlarmPersistence not found.");
                return;
            }

            if (schedulingService == null)
            {
                Debug.LogError(
                    "AlarmSystemBootstrap: AlarmSchedulingService not found.");
                return;
            }

            schedulingService.RescheduleAll();
        }
    }
}
