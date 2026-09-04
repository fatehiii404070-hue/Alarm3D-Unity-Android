using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmStopController : MonoBehaviour
    {
        [SerializeField]
        private AndroidAlarmServiceBridge androidServiceBridge;

        private void Awake()
        {
            if (androidServiceBridge == null)
            {
                androidServiceBridge =
                    FindFirstObjectByType<AndroidAlarmServiceBridge>();
            }
        }

        public void StopActiveAlarm()
        {
            if (androidServiceBridge == null)
            {
                Debug.LogError(
                    "AlarmStopController: " +
                    "AndroidAlarmServiceBridge not found.");

                return;
            }

            androidServiceBridge.StopAlarm();
        }
    }
}
