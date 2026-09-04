using System;
using UnityEngine;

namespace Alarm3D.Alarm
{
    public static class AlarmIdGenerator
    {
        public static string Create()
        {
            return Guid.NewGuid().ToString("N");
        }

        public static bool IsValid(string id)
        {
            return !string.IsNullOrWhiteSpace(id);
        }
    }
}
