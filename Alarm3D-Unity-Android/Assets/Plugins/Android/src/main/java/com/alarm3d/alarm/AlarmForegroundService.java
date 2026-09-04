package com.alarm3d.alarm;

import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.Service;
import android.content.Intent;
import android.os.Build;
import android.os.IBinder;

public final class AlarmForegroundService extends Service
{
    private static final String CHANNEL_ID = "alarm3d_alarm";
    private static final int NOTIFICATION_ID = 3001;

    @Override
    public int onStartCommand(Intent intent, int flags, int startId)
    {
        createNotificationChannel();

        Notification notification =
                new Notification.Builder(this, CHANNEL_ID)
                        .setContentTitle("هشدار")
                        .setContentText("زمان هشدار فرا رسیده است.")
                        .setSmallIcon(android.R.drawable.ic_lock_idle_alarm)
                        .setOngoing(true)
                        .build();

        startForeground(NOTIFICATION_ID, notification);

        return START_NOT_STICKY;
    }

    private void createNotificationChannel()
    {
        if (Build.VERSION.SDK_INT < Build.VERSION_CODES.O)
        {
            return;
        }

        NotificationChannel channel =
                new NotificationChannel(
                        CHANNEL_ID,
                        "Alarm3D Alarms",
                        NotificationManager.IMPORTANCE_HIGH);

        channel.setDescription(
                "Notifications used for scheduled alarms.");

        NotificationManager manager =
                getSystemService(NotificationManager.class);

        if (manager != null)
        {
            manager.createNotificationChannel(channel);
        }
    }

    @Override
    public IBinder onBind(Intent intent)
    {
        return null;
    }
}
