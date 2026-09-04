namespace Alarm3D.Alarm
{
    public static class AlarmTimeValidator
    {
        public static bool IsValid(int hour, int minute)
        {
            return hour >= 0 &&
                   hour <= 23 &&
                   minute >= 0 &&
                   minute <= 59;
        }
    }
}
