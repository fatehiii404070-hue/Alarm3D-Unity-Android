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

    private static final String EXTRA_ALARM_ID =
            "alarm_id";

    private MediaPlayer mediaPlayer;

    private String activeAlarmId = "";

    @Override
    public int onStartCommand(
            Intent intent,
            int flags,
            int startId)
    {
        if (intent != null)
        {
            String alarmId =
                    intent.getStringExtra(
                            EXTRA_ALARM_ID);

            if (alarmId != null)
            {
                activeAlarmId = alarmId;
            }
        }

        createNotificationChannel();

        Notification notification =
                new Notification.Builder(
                        this,
                        CHANNEL_ID)
                        .setContentTitle(
                                "Alarm3D")
                        .setContentText(
                                "Alarm is ringing")
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
        }
    }

    private void stopAlarmSound()
    {
        if (mediaPlayer == null)
        {
            return;
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
                        "Alarm3D Alarm",
                        NotificationManager.IMPORTANCE_HIGH);

        channel.setSound(
                android.provider.Settings.System
                        .DEFAULT_ALARM_ALERT_URI,
                new AudioAttributes.Builder()
                        .setUsage(
                                AudioAttributes.USAGE_ALARM)
                        .build());

        NotificationManager manager =
                getSystemService(
                        NotificationManager.class);

        if (manager != null)
        {
            manager.createNotificationChannel(
                    channel);
        }
    }

    public String getActiveAlarmId()
    {
        return activeAlarmId;
    }

    @Override
    public void onDestroy()
    {
        stopAlarmSound();

        super.onDestroy();
    }

    @Override
    public IBinder onBind(
            Intent intent)
    {
        return null;
    }
}
