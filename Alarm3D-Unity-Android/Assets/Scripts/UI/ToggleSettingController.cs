using System;
using UnityEngine;
using UnityEngine.UI;

namespace Alarm3D.UI
{
    [DisallowMultipleComponent]
    public sealed class ToggleSettingController : MonoBehaviour
    {
        public enum SettingType
        {
            Notifications,
            Vibration
        }

        [Header("Configuration")]
        [SerializeField]
        private SettingType settingType;

        [Header("UI")]
        [SerializeField]
        private Toggle toggle;

        private bool initialized;

        private void Awake()
        {
            if (toggle == null)
            {
                toggle = GetComponent<Toggle>();
            }
        }

        private void OnEnable()
        {
            Initialize();
        }

        private void OnDisable()
        {
            if (toggle != null)
            {
                toggle.onValueChanged.RemoveListener(OnToggleChanged);
            }

            initialized = false;
        }

        private void Initialize()
        {
            if (toggle == null)
                return;

            Settings.SettingsManager manager =
                Settings.SettingsManager.Instance;

            if (manager == null)
                return;

            toggle.onValueChanged.RemoveListener(OnToggleChanged);

            toggle.SetIsOnWithoutNotify(GetCurrentValue(manager));

            toggle.onValueChanged.AddListener(OnToggleChanged);

            initialized = true;
        }

        private bool GetCurrentValue(
            Settings.SettingsManager manager)
        {
            return settingType switch
            {
                SettingType.Notifications =>
                    manager.NotificationsEnabled,

                SettingType.Vibration =>
                    manager.VibrationEnabled,

                _ => false
            };
        }

        private void OnToggleChanged(bool value)
        {
            if (!initialized)
                return;

            Settings.SettingsManager manager =
                Settings.SettingsManager.Instance;

            if (manager == null)
                return;

            switch (settingType)
            {
                case SettingType.Notifications:
                    manager.SetNotificationsEnabled(value);
                    break;

                case SettingType.Vibration:
                    manager.SetVibrationEnabled(value);
                    break;
            }
        }
    }
}
