using System.Collections.Generic;
using UnityEngine;

namespace Alarm3D.UI
{
    public class AlarmListController : MonoBehaviour
    {
        [SerializeField]
        private AlarmCardController alarmCardPrefab;

        [SerializeField]
        private Transform contentParent;

        private readonly List<AlarmCardController> cards =
            new List<AlarmCardController>();

        private void OnEnable()
        {
            if (Alarm.AlarmManager.Instance != null)
            {
                Alarm.AlarmManager.Instance.AlarmsChanged += Refresh;
            }

            Refresh();
        }

        private void OnDisable()
        {
            if (Alarm.AlarmManager.Instance != null)
            {
                Alarm.AlarmManager.Instance.AlarmsChanged -= Refresh;
            }
        }

        public void Refresh()
        {
            ClearCards();

            if (alarmCardPrefab == null || contentParent == null)
                return;

            if (Alarm.AlarmManager.Instance == null)
                return;

            foreach (Alarm.AlarmData alarm in Alarm.AlarmManager.Instance.Alarms)
            {
                if (alarm == null)
                    continue;

                AlarmCardController card =
                    Instantiate(alarmCardPrefab, contentParent);

                card.Initialize(
                    alarm.id,
                    alarm.title,
                    alarm.hour,
                    alarm.minute,
                    alarm.enabled
                );

                cards.Add(card);
            }
        }

        private void ClearCards()
        {
            foreach (AlarmCardController card in cards)
            {
                if (card != null)
                    Destroy(card.gameObject);
            }

            cards.Clear();
        }
    }
}
