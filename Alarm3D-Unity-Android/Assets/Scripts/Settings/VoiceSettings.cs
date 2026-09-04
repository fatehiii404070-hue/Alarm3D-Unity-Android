using System;
using UnityEngine;

namespace Alarm3D.Settings
{
    public class VoiceSettings : MonoBehaviour
    {
        public static VoiceSettings Instance { get; private set; }

        public string SelectedVoiceId { get; private set; } = string.Empty;
        public string SelectedLanguage { get; private set; } = "fa";

        public event Action VoiceSettingsChanged;

        private void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
                return;
            }

            Instance = this;
            DontDestroyOnLoad(gameObject);

            Load();
        }

        public void SetVoice(string voiceId)
        {
            SelectedVoiceId = voiceId ?? string.Empty;

            SettingsStorage.SaveSelectedVoice(
                SelectedVoiceId
            );

            VoiceSettingsChanged?.Invoke();
        }

        public void SetLanguage(string language)
        {
            if (string.IsNullOrWhiteSpace(language))
                return;

            SelectedLanguage = language;

            SettingsStorage.SaveSelectedLanguage(
                SelectedLanguage
            );

            VoiceSettingsChanged?.Invoke();
        }

        private void Load()
        {
            SelectedVoiceId =
                SettingsStorage.LoadSelectedVoice();

            SelectedLanguage =
                SettingsStorage.LoadSelectedLanguage();
        }
    }
}
