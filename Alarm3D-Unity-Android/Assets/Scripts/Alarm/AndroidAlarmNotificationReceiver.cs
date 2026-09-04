using UnityEngine;
using Unity.Notifications.Android;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AndroidAlarmNotificationReceiver : MonoBehaviour
    {
        private void OnEnable()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidNotificationCenter.OnNotificationReceived += HandleNotificationReceived;
#endif
        }

        private void OnDisable()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidNotificationCenter.OnNotificationReceived -= HandleNotificationReceived;
#endif
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        private static void HandleNotificationReceived(
            AndroidNotificationIntentData data)
        {
            if (data == null)
                return;

            Debug.Log(
                $"Alarm notification received. Id: {data.Id}, Channel: {data.Channel}");
        }
#endif
    }
}
```0
