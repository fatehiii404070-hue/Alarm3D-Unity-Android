package com.alarm3d.alarm;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.os.Build;

public final class AlarmSchedulerBridge
{
    private static final int REQUEST_CODE_BASE = 4000;

    private AlarmSchedulerBridge()
    {
    }

    public static void schedule(
            Context context,
            String alarmId,
            long triggerAtMillis)
    {
        if (context == null ||
            alarmId == null ||
            alarmId.trim().isEmpty() ||
            triggerAtMillis <= System.currentTimeMillis())
        {
            return;
        }

        AlarmManager alarmManager =
                (AlarmManager) context.getSystemService(Context.ALARM_SERVICE);

        if (alarmManager == null)
        {
            return;
        }

        Intent intent =
                new Intent(context, AlarmReceiver.class);

        intent.putExtra("alarm_id", alarmId);

        int requestCode =
                REQUEST_CODE_BASE +
                Math.abs(alarmId.hashCode() % 1000000);

        PendingIntent pendingIntent =
                PendingIntent.getBroadcast(
                        context,
                        requestCode,
                        intent,
                        PendingIntent.FLAG_UPDATE_CURRENT |
                        PendingIntent.FLAG_IMMUTABLE);

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.S)
        {
            if (!alarmManager.canScheduleExactAlarms())
            {
                return;
            }
        }

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.M)
        {
            alarmManager.setExactAndAllowWhileIdle(
                    AlarmManager.RTC_WAKEUP,
                    triggerAtMillis,
                    pendingIntent);
        }
        else
        {
            alarmManager.setExact(
                    AlarmManager.RTC_WAKEUP,
                    triggerAtMillis,
                    pendingIntent);
        }
    }

    public static void cancel(
            Context context,
            String alarmId)
    {
        if (context == null ||
            alarmId == null ||
            alarmId.trim().isEmpty())
        {
            return;
        }

        Intent intent =
                new Intent(context, AlarmReceiver.class);

        int requestCode =
                REQUEST_CODE_BASE +
                Math.abs(alarmId.hashCode() % 1000000);

        PendingIntent pendingIntent =
                PendingIntent.getBroadcast(
                        context,
                        requestCode,
                        intent,
                        PendingIntent.FLAG_UPDATE_CURRENT |
                        PendingIntent.FLAG_IMMUTABLE);

        AlarmManager alarmManager =
                (AlarmManager) context.getSystemService(Context.ALARM_SERVICE);

        if (alarmManager != null)
        {
            alarmManager.cancel(pendingIntent);
        }

        pendingIntent.cancel();
    }
    }
