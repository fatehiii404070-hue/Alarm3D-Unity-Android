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

        AlarmSchedulerBridge.rescheduleAll(
                context.getApplicationContext());
    }
}
