using UnityEngine;

namespace Alarm3D.Alarm
{
    public class AlarmController : MonoBehaviour
    {
        public bool IsAlarmEnabled { get; private set; }

        public void SetAlarmEnabled(bool enabled)
        {
            IsAlarmEnabled = enabled;
        }
    }
}
