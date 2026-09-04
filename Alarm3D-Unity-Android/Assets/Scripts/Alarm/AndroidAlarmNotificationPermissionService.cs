using UnityEngine;
using Unity.Notifications.Android;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AndroidAlarmNotificationPermissionService : MonoBehaviour
    {
        public void RequestPermission()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (!AndroidNotificationCenter.UserPermissionToPost)
            {
                AndroidNotificationCenter.RequestPermission();
            }
#endif
        }
    }
}
