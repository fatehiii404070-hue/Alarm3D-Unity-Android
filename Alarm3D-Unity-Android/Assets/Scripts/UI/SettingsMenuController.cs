using UnityEngine;
using UnityEngine.SceneManagement;

namespace Alarm3D.UI
{
    public class SettingsMenuController : MonoBehaviour
    {
        [SerializeField]
        private string mainSceneName = "Main";

        public void OpenMain()
        {
            if (string.IsNullOrWhiteSpace(mainSceneName))
                return;

            SceneManager.LoadScene(mainSceneName);
        }
    }
}
