using System;
using UnityEngine;

namespace Alarm3D.Audio
{
    [Serializable]
    public class AudioClipData
    {
        public string id;
        public string displayName;
        public AudioClip clip;
        public bool isRecorded;
        public bool isGenerated;
    }
}
