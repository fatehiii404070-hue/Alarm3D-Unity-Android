package com.alarm3d.alarm;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.os.Build;

public final class AlarmReceiver extends BroadcastReceiver
{
    @Override
    public void onReceive(
            Context context,
            Intent intent)
    {
        if (context == null)
        {
            return;
        }

        Intent serviceIntent =
                new Intent(
                        context,
                        AlarmForegroundService.class);

        if (intent != null)
        {
            String alarmId =
                    intent.getStringExtra("alarm_id");

            if (alarmId != null)
            {
                serviceIntent.putExtra(
                        "alarm_id",
                        alarmId);
            }
        }

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.O)
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
