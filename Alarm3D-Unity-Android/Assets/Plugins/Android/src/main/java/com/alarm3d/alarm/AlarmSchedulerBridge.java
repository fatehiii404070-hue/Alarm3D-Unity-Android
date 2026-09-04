package com.alarm3d.alarm;

import android.app.AlarmManager;
import android.app.PendingIntent;
import android.content.Context;
import android.content.Intent;
import android.content.SharedPreferences;
import android.os.Build;

import org.json.JSONArray;
import org.json.JSONException;
import org.json.JSONObject;

public final class AlarmSchedulerBridge
{
    private static final String PREFS_NAME =
            "Alarm3D.AlarmScheduler";

    private static final String PREFS_ALARMS =
            "scheduled_alarms";

    private static final int REQUEST_CODE_BASE = 4000;

    private AlarmSchedulerBridge()
    {
    }

    public static boolean schedule(
            Context context,
            String alarmId,
            long triggerAtMillis)
    {
        if (context == null ||
            alarmId == null ||
            alarmId.trim().isEmpty() ||
            triggerAtMillis <= System.currentTimeMillis())
        {
            return false;
        }

        AlarmManager alarmManager =
                (AlarmManager) context.getSystemService(
                        Context.ALARM_SERVICE);

        if (alarmManager == null)
        {
            return false;
        }

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.S &&
            !alarmManager.canScheduleExactAlarms())
        {
            return false;
        }

        PendingIntent pendingIntent =
                createPendingIntent(
                        context,
                        alarmId);

        if (pendingIntent == null)
        {
            return false;
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

        saveAlarm(
                context,
                alarmId,
                triggerAtMillis);

        return true;
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

        AlarmManager alarmManager =
                (AlarmManager) context.getSystemService(
                        Context.ALARM_SERVICE);

        PendingIntent pendingIntent =
                createPendingIntent(
                        context,
                        alarmId);

        if (alarmManager != null &&
            pendingIntent != null)
        {
            alarmManager.cancel(pendingIntent);
            pendingIntent.cancel();
        }

        removeAlarm(
                context,
                alarmId);
    }

    public static void rescheduleAll(Context context)
    {
        if (context == null)
        {
            return;
        }

        AlarmManager alarmManager =
                (AlarmManager) context.getSystemService(
                        Context.ALARM_SERVICE);

        if (alarmManager == null)
        {
            return;
        }

        if (Build.VERSION.SDK_INT >= Build.VERSION_CODES.S &&
            !alarmManager.canScheduleExactAlarms())
        {
            return;
        }

        JSONArray alarms =
                loadAlarms(context);

        JSONArray remaining =
                new JSONArray();

        long now = System.currentTimeMillis();

        for (int i = 0; i < alarms.length(); i++)
        {
            try
            {
                JSONObject alarm =
                        alarms.getJSONObject(i);

                String alarmId =
                        alarm.optString("alarm_id", "");

                long triggerAtMillis =
                        alarm.optLong(
                                "trigger_at_millis",
                                0L);

                if (alarmId.trim().isEmpty() ||
                    triggerAtMillis <= now)
                {
                    continue;
                }

                PendingIntent pendingIntent =
                        createPendingIntent(
                                context,
                                alarmId);

                if (pendingIntent == null)
                {
                    continue;
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

                remaining.put(alarm);
            }
            catch (JSONException exception)
            {
                exception.printStackTrace();
            }
        }

        saveAlarms(
                context,
                remaining);
    }

    private static PendingIntent createPendingIntent(
            Context context,
            String alarmId)
    {
        if (context == null ||
            alarmId == null ||
            alarmId.trim().isEmpty())
        {
            return null;
        }

        Intent intent =
                new Intent(
                        context,
                        AlarmReceiver.class);

        intent.putExtra(
                "alarm_id",
                alarmId);

        int requestCode =
                REQUEST_CODE_BASE +
                Math.abs(
                        alarmId.hashCode() % 1000000);

        return PendingIntent.getBroadcast(
                context,
                requestCode,
                intent,
                PendingIntent.FLAG_UPDATE_CURRENT |
                PendingIntent.FLAG_IMMUTABLE);
    }

    private static void saveAlarm(
            Context context,
            String alarmId,
            long triggerAtMillis)
    {
        JSONArray alarms =
                loadAlarms(context);

        JSONArray updated =
                new JSONArray();

        for (int i = 0; i < alarms.length(); i++)
        {
            try
            {
                JSONObject alarm =
                        alarms.getJSONObject(i);

                if (alarmId.equals(
                        alarm.optString("alarm_id", "")))
                {
                    continue;
                }

                updated.put(alarm);
            }
            catch (JSONException exception)
            {
                exception.printStackTrace();
            }
        }

        JSONObject newAlarm =
                new JSONObject();

        try
        {
            newAlarm.put(
                    "alarm_id",
                    alarmId);

            newAlarm.put(
                    "trigger_at_millis",
                    triggerAtMillis);

            updated.put(newAlarm);
        }
        catch (JSONException exception)
        {
            exception.printStackTrace();
            return;
        }

        saveAlarms(
                context,
                updated);
    }

    private static void removeAlarm(
            Context context,
            String alarmId)
    {
        JSONArray alarms =
                loadAlarms(context);

        JSONArray updated =
                new JSONArray();

        for (int i = 0; i < alarms.length(); i++)
        {
            try
            {
                JSONObject alarm =
                        alarms.getJSONObject(i);

                if (!alarmId.equals(
                        alarm.optString("alarm_id", "")))
                {
                    updated.put(alarm);
                }
            }
            catch (JSONException exception)
            {
                exception.printStackTrace();
            }
        }

        saveAlarms(
                context,
                updated);
    }

    private static JSONArray loadAlarms(
            Context context)
    {
        SharedPreferences preferences =
                context.getSharedPreferences(
                        PREFS_NAME,
                        Context.MODE_PRIVATE);

        String json =
                preferences.getString(
                        PREFS_ALARMS,
                        "[]");

        try
        {
            return new JSONArray(json);
        }
        catch (JSONException exception)
        {
            exception.printStackTrace();
            return new JSONArray();
        }
    }

    private static void saveAlarms(
            Context context,
            JSONArray alarms)
    {
        SharedPreferences preferences =
                context.getSharedPreferences(
                        PREFS_NAME,
                        Context.MODE_PRIVATE);

        preferences.edit()
                .putString(
                        PREFS_ALARMS,
                        alarms.toString())
                .apply();
    }
}
