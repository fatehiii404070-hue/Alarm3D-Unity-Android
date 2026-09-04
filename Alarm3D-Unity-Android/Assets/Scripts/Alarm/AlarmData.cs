using System;
using UnityEngine;

namespace Alarm3D.Alarm
{
    [Serializable]
    public class AlarmData
    {
        public string id;
        public string title;
        public int hour;
        public int minute;
        public bool enabled;
        public string soundId;
    }
}
