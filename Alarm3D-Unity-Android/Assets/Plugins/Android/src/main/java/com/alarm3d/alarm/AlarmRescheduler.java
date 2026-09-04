package com.alarm3d.alarm;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Build;

public final class AlarmReceiver extends BroadcastReceiver
{
    private static final String EXTRA_ALARM_ID =
            "alarm_id";

    @Override
    public void onReceive(
            Context context,
            Intent intent)
    {
        if (context == null)
        {
            return;
        }

        String alarmId = "";

        if (intent != null)
        {
            String receivedId =
                    intent.getStringExtra(
                            EXTRA_ALARM_ID);

            if (receivedId != null)
            {
                alarmId = receivedId;
            }
        }

        Intent serviceIntent =
                new Intent(
                        context,
                        AlarmForegroundService.class);

        serviceIntent.putExtra(
                EXTRA_ALARM_ID,
                alarmId);

        if (Build.VERSION.SDK_INT >=
                Build.VERSION_CODES.O)
        {
            context.startForegroundService(
                    serviceIntent);
        }
        else
        {
            context.startService(
                    serviceIntent);
        }
    }
}
