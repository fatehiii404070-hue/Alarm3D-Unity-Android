using System;
using UnityEngine;

namespace Alarm3D.Audio
{
    [Serializable]
    public sealed class RecordedVoiceAsset
    {
        [SerializeField]
        private string id;

        [SerializeField]
        private string displayName;

        [SerializeField]
        private string fileName;

        [SerializeField]
        private long createdAtTicks;

        public string Id => id;
        public string DisplayName => displayName;
        public string FileName => fileName;
        public DateTime CreatedAt =>
            new DateTime(createdAtTicks, DateTimeKind.Local);

        public RecordedVoiceAsset(
            string id,
            string displayName,
            string fileName)
        {
            this.id = id ?? string.Empty;
            this.displayName = displayName ?? string.Empty;
            this.fileName = fileName ?? string.Empty;
            createdAtTicks = DateTime.Now.Ticks;
        }

        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(id) &&
                   !string.IsNullOrWhiteSpace(displayName) &&
                   !string.IsNullOrWhiteSpace(fileName) &&
                   createdAtTicks > 0;
        }
    }
}
