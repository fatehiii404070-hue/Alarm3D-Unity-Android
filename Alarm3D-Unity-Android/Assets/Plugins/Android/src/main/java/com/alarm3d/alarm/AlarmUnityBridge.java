package com.alarm3d.alarm;

import com.unity3d.player.UnityPlayer;

public final class AlarmUnityBridge
{
    private static final String UNITY_OBJECT =
            "AlarmAndroidReceiver";

    private static final String UNITY_METHOD =
            "OnAlarmTriggered";

    private AlarmUnityBridge()
    {
    }

    public static void sendAlarmToUnity(
            String alarmId)
    {
        if (alarmId == null)
        {
            alarmId = "";
        }

        try
        {
            UnityPlayer.UnitySendMessage(
                    UNITY_OBJECT,
                    UNITY_METHOD,
                    alarmId);
        }
        catch (Exception exception)
        {
            exception.printStackTrace();
        }
    }
}
