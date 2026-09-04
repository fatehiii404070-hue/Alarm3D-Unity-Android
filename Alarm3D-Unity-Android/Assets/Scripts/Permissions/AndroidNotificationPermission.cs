using UnityEngine;
using Unity.Notifications.Android;

namespace Alarm3D.Permissions
{
    [DisallowMultipleComponent]
    public sealed class AndroidNotificationPermission : MonoBehaviour
    {
        private void Start()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            RequestPermission();
#endif
        }

#if UNITY_ANDROID && !UNITY_EDITOR
        private static void RequestPermission()
        {
            AndroidNotificationCenter.RequestPermission();
        }
#endif
    }
}
