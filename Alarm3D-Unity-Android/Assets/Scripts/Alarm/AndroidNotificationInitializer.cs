using UnityEngine;
using Unity.Notifications.Android;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AndroidNotificationInitializer : MonoBehaviour
    {
        private const string ChannelId = "alarm3d_alarm";

        private void Awake()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            CreateAlarmChannel();
#endif
        }

        private static void CreateAlarmChannel()
        {
            var channel = new AndroidNotificationChannel
            {
                Id = ChannelId,
                Name = "Alarm3D Alarms",
                Description = "Notifications used for scheduled alarms.",
                Importance = Importance.High,
                CanBypassDnd = true,
                EnableLights = true,
                EnableVibration = true,
                LockScreenVisibility = LockScreenVisibility.Public
            };

            AndroidNotificationCenter.RegisterNotificationChannel(channel);
        }
    }
}
