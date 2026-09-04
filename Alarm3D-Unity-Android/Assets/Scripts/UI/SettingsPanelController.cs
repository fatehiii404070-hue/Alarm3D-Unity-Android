using UnityEngine;

namespace Alarm3D.UI
{
    public class SettingsPanelController : MonoBehaviour
    {
        [SerializeField]
        private GameObject settingsPanel;

        [SerializeField]
        private GameObject mainPanel;

        public void OpenSettings()
        {
            if (mainPanel != null)
                mainPanel.SetActive(false);

            if (settingsPanel != null)
                settingsPanel.SetActive(true);
        }

        public void CloseSettings()
        {
            if (settingsPanel != null)
                settingsPanel.SetActive(false);

            if (mainPanel != null)
                mainPanel.SetActive(true);
        }

        public void ToggleSettings()
        {
            bool isOpen =
                settingsPanel != null &&
                settingsPanel.activeSelf;

            if (isOpen)
                CloseSettings();
            else
                OpenSettings();
        }
    }
}
