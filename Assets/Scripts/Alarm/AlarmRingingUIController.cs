using UnityEngine;
using UnityEngine.UI;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmRingingUIController : MonoBehaviour
    {
        [SerializeField]
        private AlarmRuntimeState runtimeState;

        [SerializeField]
        private GameObject ringingPanel;

        [SerializeField]
        private Button stopButton;

        private void Awake()
        {
            if (runtimeState == null)
            {
                runtimeState =
                    FindFirstObjectByType<AlarmRuntimeState>();
            }

            if (stopButton != null)
            {
                stopButton.onClick.AddListener(
                    StopAlarm);
            }

            SetPanelVisible(false);
        }

        private void OnEnable()
        {
            if (runtimeState != null)
            {
                runtimeState.RingingStateChanged +=
                    HandleRingingStateChanged;
            }
        }

        private void OnDisable()
        {
            if (runtimeState != null)
            {
                runtimeState.RingingStateChanged -=
                    HandleRingingStateChanged;
            }
        }

        private void HandleRingingStateChanged(
            bool isRinging)
        {
            SetPanelVisible(isRinging);
        }

        public void StopAlarm()
        {
            if (runtimeState == null)
            {
                return;
            }

            runtimeState.StopRinging();
            SetPanelVisible(false);
        }

        private void SetPanelVisible(bool visible)
        {
            if (ringingPanel != null)
            {
                ringingPanel.SetActive(visible);
            }
        }
    }
}
