using UnityEngine;
using UnityEngine.SceneManagement;

namespace Alarm3D.UI
{
    public class MainMenuController : MonoBehaviour
    {
        [SerializeField]
        private string settingsSceneName = "Settings";

        public void OpenSettings()
        {
            if (string.IsNullOrWhiteSpace(settingsSceneName))
                return;

            SceneManager.LoadScene(settingsSceneName);
        }

        public void ExitApplication()
        {
            Application.Quit();
        }
    }
}
