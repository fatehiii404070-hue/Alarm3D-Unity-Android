using UnityEngine;

namespace Alarm3D.Alarm
{
    [CreateAssetMenu(
        fileName = "AndroidAlarmNotificationSettings",
        menuName = "Alarm3D/Alarm/Android Alarm Notification Settings")]
    public sealed class AndroidAlarmNotificationSettings : ScriptableObject
    {
        [SerializeField]
        private string channelId = "alarm3d_alarm";

        [SerializeField]
        private string channelName = "Alarm3D Alarms";

        [SerializeField]
        private string channelDescription =
            "Notifications used for scheduled alarms.";

        [SerializeField]
        private int channelImportance = 4;

        [SerializeField]
        private string notificationTitle = "هشدار";

        [SerializeField]
        private string notificationText = "زمان هشدار فرا رسیده است.";

        public string ChannelId => channelId;
        public string ChannelName => channelName;
        public string ChannelDescription => channelDescription;
        public int ChannelImportance => channelImportance;
        public string NotificationTitle => notificationTitle;
        public string NotificationText => notificationText;
    }
}
