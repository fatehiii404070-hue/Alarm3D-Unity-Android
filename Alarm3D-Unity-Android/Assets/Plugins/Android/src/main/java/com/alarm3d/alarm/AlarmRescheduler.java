package com.alarm3d.alarm;

import android.content.Context;

public final class AlarmRescheduler
{
    private AlarmRescheduler()
    {
    }

    public static void reschedule(Context context)
    {
        if (context == null)
        {
            return;
        }

        // Re-scheduling will be connected to the persistent Unity alarm store.
        // This entry point is intentionally kept safe until that integration
        // is implemented.
    }
}
