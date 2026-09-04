using UnityEngine;
using UnityEngine.UI;

namespace Alarm3D.UI
{
    public class AlarmCardController : MonoBehaviour
    {
        [SerializeField]
        private Text titleText;

        [SerializeField]
        private Text timeText;

        [SerializeField]
        private Toggle enabledToggle;

        private string alarmId;

        public void Initialize(string id, string title, int hour, int minute, bool enabled)
        {
            alarmId = id;

            if (titleText != null)
                titleText.text = title;

            if (timeText != null)
                timeText.text = $"{hour:00}:{minute:00}";

            if (enabledToggle != null)
            {
                enabledToggle.SetIsOnWithoutNotify(enabled);
                enabledToggle.onValueChanged.RemoveListener(OnToggleChanged);
                enabledToggle.onValueChanged.AddListener(OnToggleChanged);
            }
        }

        private void OnToggleChanged(bool enabled)
        {
            if (Alarm.AlarmManager.Instance == null)
                return;

            Alarm.AlarmManager.Instance.SetAlarmEnabled(
                alarmId,
                enabled
            );
        }

        private void OnDestroy()
        {
            if (enabledToggle != null)
                enabledToggle.onValueChanged.RemoveListener(OnToggleChanged);
        }
    }
}
