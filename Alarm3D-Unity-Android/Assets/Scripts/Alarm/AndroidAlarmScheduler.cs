using System;
using UnityEngine;
using Unity.Notifications.Android;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AndroidAlarmScheduler : MonoBehaviour
    {
        private const string ChannelId = "alarm3d_alarm";

        public int Schedule(
            string alarmId,
            string title,
            string text,
            DateTime fireTime)
        {
#if UNITY_ANDROID && !UNITY_EDITOR
            var notification = new AndroidNotification
            {
                Title = title,
                Text = text,
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
            if (notificationId >= 0)
            {
                AndroidNotificationCenter.CancelNotification(notificationId);
            }
#endif
        }
    }
}
