package com.alarm3d.alarm;

import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;

public final class AlarmReceiver extends BroadcastReceiver
{
    @Override
    public void onReceive(Context context, Intent intent)
    {
        if (context == null)
        {
            return;
        }

        Intent serviceIntent =
                new Intent(context, AlarmForegroundService.class);

        if (intent != null)
        {
            serviceIntent.putExtras(intent);
        }

        context.startForegroundService(serviceIntent);
    }
}
