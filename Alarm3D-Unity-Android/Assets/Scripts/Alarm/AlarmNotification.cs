using UnityEngine;

namespace Alarm3D.Alarm
{
    public class AlarmNotification : MonoBehaviour
    {
        public void ShowAlarmNotification(string title, string message)
        {
            Debug.Log($"Alarm Notification: {title} - {message}");
        }
    }
}
