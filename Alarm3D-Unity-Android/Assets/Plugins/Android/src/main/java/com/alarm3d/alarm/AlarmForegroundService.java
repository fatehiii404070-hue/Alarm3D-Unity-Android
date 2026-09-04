package com.alarm3d.alarm;

import android.app.Notification;
import android.app.NotificationChannel;
import android.app.NotificationManager;
import android.app.Service;
import android.content.Intent;
import android.media.AudioAttributes;
import android.media.MediaPlayer;
import android.net.Uri;
import android.os.Build;
import android.os.IBinder;

public final class AlarmForegroundService extends Service
{
    private static final String CHANNEL_ID =
            "alarm3d_alarm";

    private static final int NOTIFICATION_ID =
            3001;

    private static final String ACTION_STOP =
            "com.alarm3d.alarm.STOP_ALARM";

    private MediaPlayer mediaPlayer;

    @Override
    public int onStartCommand(
            Intent intent,
            int flags,
            int startId)
    {
        if (intent != null &&
            ACTION_STOP.equals(intent.getAction()))
        {
            stopAlarmSound();

            if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.N)
            {
                stopForeground(STOP_FOREGROUND_REMOVE);
            }
            else
            {
                stopForeground(true);
            }

            stopSelf(startId);

            return START_NOT_STICKY;
        }

        createNotificationChannel();

        Notification notification =
                new Notification.Builder(
                        this,
                        CHANNEL_ID)
                        .setContentTitle("هشدار")
                        .setContentText(
                                "زمان هشدار فرا رسیده است.")
                        .setSmallIcon(
                                android.R.drawable
                                        .ic_lock_idle_alarm)
                        .setOngoing(true)
                        .setCategory(
                                Notification.CATEGORY_ALARM)
                        .build();

        startForeground(
                NOTIFICATION_ID,
                notification);

        startAlarmSound();

        return START_NOT_STICKY;
    }

    private void startAlarmSound()
    {
        stopAlarmSound();

        try
        {
            Uri soundUri =
                    android.provider.Settings.System
                            .DEFAULT_ALARM_ALERT_URI;

            mediaPlayer =
                    new MediaPlayer();

            mediaPlayer.setAudioAttributes(
                    new AudioAttributes.Builder()
                            .setUsage(
                                    AudioAttributes.USAGE_ALARM)
                            .setContentType(
                                    AudioAttributes
                                            .CONTENT_TYPE_SONIFICATION)
                            .build());

            mediaPlayer.setDataSource(
                    this,
                    soundUri);

            mediaPlayer.setLooping(true);

            mediaPlayer.setOnPreparedListener(
                    MediaPlayer::start);

            mediaPlayer.prepareAsync();
        }
        catch (Exception exception)
        {
            exception.printStackTrace();
            stopAlarmSound();
        }
    }

    private void stopAlarmSound()
    {
        if (mediaPlayer == null)
        {
            return;
        }

        try
        {
            if (mediaPlayer.isPlaying())
            {
                mediaPlayer.stop();
            }
        }
        catch (IllegalStateException ignored)
        {
        }

        mediaPlayer.release();
        mediaPlayer = null;
    }

    private void createNotificationChannel()
    {
        if (Build.VERSION.SDK_INT <
                Build.VERSION_CODES.O)
        {
            return;
        }

        NotificationChannel channel =
                new NotificationChannel(
                        CHANNEL_ID,
                        "Alarm3D Alarms",
                        NotificationManager
                                .IMPORTANCE_HIGH);

        channel.setDescription(
                "Notifications used for scheduled alarms.");

        channel.setSound(
                android.provider.Settings.System
                        .DEFAULT_ALARM_ALERT_URI,
                new AudioAttributes.Builder()
                        .setUsage(
                                AudioAttributes.USAGE_ALARM)
                        .build());

        channel.enableVibration(true);

        channel.setLockscreenVisibility(
                Notification.VISIBILITY_PUBLIC);

        NotificationManager manager =
                getSystemService(
                        NotificationManager.class);

        if (manager != null)
        {
            manager.createNotificationChannel(
                    channel);
        }
    }

    @Override
    public void onDestroy()
    {
        stopAlarmSound();

        NotificationManager manager =
                getSystemService(
                        NotificationManager.class);

        if (manager != null)
        {
            manager.cancel(NOTIFICATION_ID);
        }

        super.onDestroy();
    }

    @Override
    public IBinder onBind(Intent intent)
    {
        return null;
    }
}
