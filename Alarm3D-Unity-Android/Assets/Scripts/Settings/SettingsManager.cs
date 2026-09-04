using System;
using UnityEngine;

namespace Alarm3D.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        public static SettingsManager Instance { get; private set; }

        public bool NotificationsEnabled { get; private set; } = true;
        public bool VibrationEnabled { get; private set; } = true;
        public float MasterVolume { get; private set; } = 1f;

        public event Action SettingsChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            LoadSettings();
        }

        public void SetNotificationsEnabled(bool enabled)
        {
            NotificationsEnabled = enabled;
            SaveSettings();
        }

        public void SetVibrationEnabled(bool enabled)
        {
            VibrationEnabled = enabled;
            SaveSettings();
        }

        public void SetMasterVolume(float volume)
        {
            MasterVolume = Mathf.Clamp01(volume);
            SaveSettings();
        }

        private void SaveSettings()
        {
            PlayerPrefs.SetInt(
                "NotificationsEnabled",
                NotificationsEnabled ? 1 : 0
            );

            PlayerPrefs.SetInt(
                "VibrationEnabled",
                VibrationEnabled ? 1 : 0
            );

            PlayerPrefs.SetFloat(
                "MasterVolume",
                MasterVolume
            );

            PlayerPrefs.Save();

            SettingsChanged?.Invoke();
        }

        private void LoadSettings()
        {
            NotificationsEnabled =
                PlayerPrefs.GetInt("NotificationsEnabled", 1) == 1;

            VibrationEnabled =
                PlayerPrefs.GetInt("VibrationEnabled", 1) == 1;

            MasterVolume =
                PlayerPrefs.GetFloat("MasterVolume", 1f);
        }
    }
}
