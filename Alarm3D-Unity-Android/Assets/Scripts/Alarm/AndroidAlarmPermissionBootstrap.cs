using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AndroidAlarmPermissionBootstrap : MonoBehaviour
    {
        private void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            GetComponent<AndroidAlarmNotificationPermissionService>()
                ?.RequestPermission();
#endif
        }
    }
}
