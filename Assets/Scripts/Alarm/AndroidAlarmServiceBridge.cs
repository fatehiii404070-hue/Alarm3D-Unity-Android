using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AndroidAlarmServiceBridge : MonoBehaviour
    {
        private const string ServiceClass =
            "com.alarm3d.alarm.AlarmForegroundService";

        private const string StopAction =
            "com.alarm3d.alarm.STOP_ALARM";

        public void StopAlarm()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (var unityPlayer =
                   new AndroidJavaClass(
                       "com.unity3d.player.UnityPlayer"))
            using (var activity =
                   unityPlayer.GetStatic<AndroidJavaObject>(
                       "currentActivity"))
            using (var intent =
                   new AndroidJavaObject(
                       "android.content.Intent"))
            {
                intent.Call<AndroidJavaObject>(
                    "setClassName",
                    activity,
                    ServiceClass);

                intent.Call<AndroidJavaObject>(
                    "setAction",
                    StopAction);

                activity.Call<AndroidJavaObject>(
                    "startService",
                    intent);
            }
#endif
        }
    }
}
