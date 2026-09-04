using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmRingingCommandBridge : MonoBehaviour
    {
        [SerializeField]
        private AlarmRingingManager ringingManager;

        private void Awake()
        {
            if (ringingManager == null)
            {
                ringingManager =
                    FindFirstObjectByType<AlarmRingingManager>();
            }
        }

        public void StartAlarm(string alarmId)
        {
            if (ringingManager == null)
            {
                Debug.LogError(
                    "AlarmRingingCommandBridge: " +
                    "AlarmRingingManager not found.");

                return;
            }

            ringingManager.StartAlarm(alarmId);
        }

        public void StopAlarm()
        {
            if (ringingManager == null)
            {
                return;
            }

            ringingManager.StopAlarm();
        }
    }
}
