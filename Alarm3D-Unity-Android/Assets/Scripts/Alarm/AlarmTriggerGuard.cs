using System.Collections.Generic;
using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmTriggerGuard : MonoBehaviour
    {
        private readonly HashSet<string> triggeredAlarmIds =
            new HashSet<string>();

        public bool CanTrigger(string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return false;

            return !triggeredAlarmIds.Contains(alarmId);
        }

        public void MarkTriggered(string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return;

            triggeredAlarmIds.Add(alarmId);
        }

        public void ResetTrigger(string alarmId)
        {
            if (string.IsNullOrWhiteSpace(alarmId))
                return;

            triggeredAlarmIds.Remove(alarmId);
        }

        public void ResetAll()
        {
            triggeredAlarmIds.Clear();
        }
    }
}
