using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AndroidExactAlarmPermission : MonoBehaviour
    {
        private const string ExactAlarmSettingsAction =
            "android.settings.REQUEST_SCHEDULE_EXACT_ALARM";

        public bool IsGranted()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (var unityPlayer =
                   new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity =
                   unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
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

        public void OpenSettings()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            using (var unityPlayer =
                   new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            using (var activity =
                   unityPlayer.GetStatic<AndroidJavaObject>("currentActivity"))
            using (var intent =
                   new AndroidJavaObject(
                       "android.content.Intent",
                       ExactAlarmSettingsAction))
            {
                string packageName =
                    activity.Call<string>("getPackageName");

                intent.Call<AndroidJavaObject>(
                    "setData",
                    AndroidUriFromPackage(packageName));

                activity.Call(
                    "startActivity",
                    intent);
            }
#endif
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        private static AndroidJavaObject AndroidUriFromPackage(
            string packageName)
        {
            using (var uriClass =
                   new AndroidJavaClass("android.net.Uri"))
            {
                return uriClass.CallStatic<AndroidJavaObject>(
                    "parse",
                    "package:" + packageName);
            }
        }
#endif
    }
}
