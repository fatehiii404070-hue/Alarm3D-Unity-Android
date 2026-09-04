using System;
using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AndroidAlarmSchedulerBridge : MonoBehaviour
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        private const string BridgeClass =
            "com.alarm3d.alarm.AlarmSchedulerBridge";
#endif

        public void Schedule(string alarmId, DateTime fireTime)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return;

            if (fireTime <= DateTime.Now)
                return;

#if UNITY_ANDROID && !UNITY_EDITOR
            using (var unityPlayer =
                   new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity =
                   unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var bridge =
                   new AndroidJavaClass(BridgeClass))
            {
                bridge.CallStatic(
                    "schedule",
                    activity,
                    alarmId,
                    new DateTimeOffset(fireTime).ToUnixTimeMilliseconds());
            }
#endif
        }

        public void Cancel(string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return;

#if UNITY_ANDROID && !UNITY_EDITOR
            using (var unityPlayer =
                   new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity =
                   unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var bridge =
                   new AndroidJavaClass(BridgeClass))
            {
                bridge.CallStatic(
                    "cancel",
                    activity,
                    alarmId);
            }
#endif
        }
    }
}
