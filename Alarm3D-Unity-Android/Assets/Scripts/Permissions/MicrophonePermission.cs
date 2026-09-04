using UnityEngine;

#if UNITY_ANDROID
using UnityEngine.Android;
#endif

namespace Alarm3D.Permissions
{
    public class MicrophonePermission : MonoBehaviour
    {
        private const string MicrophonePermissionName =
            "android.permission.RECORD_AUDIO";

        public bool HasPermission()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            return Permission.HasUserAuthorizedPermission(
                MicrophonePermissionName
            );
#else
            return true;
#endif
        }

        public void RequestPermission()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (!HasPermission())
            {
                Permission.RequestUserPermission(
                    MicrophonePermissionName
                );
            }
#endif
        }
    }
}
