using UnityEngine;
using UnityEngine.UI;

namespace Alarm3D.UI
{
    public class VolumeSliderController : MonoBehaviour
    {
        [SerializeField]
        private Slider volumeSlider;

        private void Start()
        {
            if (volumeSlider == null)
                return;

            float currentVolume = 1f;

            if (Settings.SettingsManager.Instance != null)
            {
                currentVolume =
                    Settings.SettingsManager.Instance.MasterVolume;
            }

            volumeSlider.SetValueWithoutNotify(currentVolume);
            volumeSlider.onValueChanged.AddListener(OnVolumeChanged);
        }

        private void OnVolumeChanged(float value)
        {
            if (Settings.SettingsManager.Instance == null)
                return;

            Settings.SettingsManager.Instance.SetMasterVolume(value);
        }

        private void OnDestroy()
        {
            if (volumeSlider != null)
                volumeSlider.onValueChanged.RemoveListener(OnVolumeChanged);
        }
    }
}
