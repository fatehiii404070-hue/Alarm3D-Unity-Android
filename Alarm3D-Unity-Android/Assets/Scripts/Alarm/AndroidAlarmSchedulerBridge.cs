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

        public bool CanScheduleExactAlarms()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (var unityPlayer =
                   new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity =
                   unityPlayer.GetStatic<AndroidJavaObject>(
                       "currentActivity"))
            using (var alarmManager =
                   activity.Call<AndroidJavaObject>(
                       "getSystemService",
                       "alarm"))
            {
                if (alarmManager == null)
                    return false;

                return alarmManager.Call<bool>(
                    "canScheduleExactAlarms");
            }
#else
            return true;
#endif
        }

        public bool Schedule(
            string alarmId,
            DateTime fireTime)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return false;

            if (fireTime <= DateTime.Now)
                return false;

#if UNITY_ANDROID && !UNITY_EDITOR
            if (!CanScheduleExactAlarms())
            {
                Debug.LogWarning(
                    "Exact alarm permission is not granted.");

                return false;
            }

            using (var unityPlayer =
                   new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity =
                   unityPlayer.GetStatic<AndroidJavaObject>(
                       "currentActivity"))
            using (var bridge =
                   new AndroidJavaClass(BridgeClass))
            {
                return bridge.CallStatic<bool>(
                    "schedule",
                    activity,
                    alarmId,
                    new DateTimeOffset(fireTime)
                        .ToUnixTimeMilliseconds());
            }
#else
            return true;
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
                   unityPlayer.GetStatic<AndroidJavaObject>(
                       "currentActivity"))
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
