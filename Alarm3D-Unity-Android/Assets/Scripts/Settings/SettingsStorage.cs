using UnityEngine;

namespace Alarm3D.Settings
{
    public static class SettingsStorage
    {
        private const string SelectedVoiceKey = "SelectedVoice";
        private const string SelectedLanguageKey = "SelectedLanguage";

        public static void SaveSelectedVoice(string voiceId)
        {
            PlayerPrefs.SetString(SelectedVoiceKey, voiceId ?? string.Empty);
            PlayerPrefs.Save();
        }

        public static string LoadSelectedVoice()
        {
            return PlayerPrefs.GetString(SelectedVoiceKey, string.Empty);
        }

        public static void SaveSelectedLanguage(string language)
        {
            PlayerPrefs.SetString(
                SelectedLanguageKey,
                language ?? "fa"
            );

            PlayerPrefs.Save();
        }

        public static string LoadSelectedLanguage()
        {
            return PlayerPrefs.GetString(
                SelectedLanguageKey,
                "fa"
            );
        }
    }
}
