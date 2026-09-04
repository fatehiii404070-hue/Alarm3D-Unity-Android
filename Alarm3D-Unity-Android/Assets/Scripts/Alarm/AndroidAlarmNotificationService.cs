using System;
using UnityEngine;
using Unity.Notifications.Android;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AndroidAlarmNotificationService : MonoBehaviour
    {
        private const string ChannelId = "alarm3d_alarm";

        public int Schedule(
            string alarmId,
            string title,
            string message,
            DateTime fireTime)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (string.IsNullOrWhiteSpace(alarmId))
                return -1;

            if (fireTime <= DateTime.Now)
                return -1;

            var notification = new AndroidNotification
            {
                Title = string.IsNullOrWhiteSpace(title)
                    ? "هشدار"
                    : title,
                Text = string.IsNullOrWhiteSpace(message)
                    ? "زمان هشدار فرا رسیده است."
                    : message,
                FireTime = fireTime,
                ShouldAutoCancel = false,
                ShowTimestamp = true
            };

            return AndroidNotificationCenter.SendNotification(
                notification,
                ChannelId);
#else
            return -1;
#endif
        }

        public void Cancel(int notificationId)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            if (notificationId < 0)
                return;

            AndroidNotificationCenter.CancelNotification(notificationId);
#endif
        }

        public void CancelAll()
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            AndroidNotificationCenter.CancelAllNotifications();
#endif
        }
    }
}
