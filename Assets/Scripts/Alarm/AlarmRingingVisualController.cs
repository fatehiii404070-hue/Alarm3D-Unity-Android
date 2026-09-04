using UnityEngine;

namespace Alarm3D.Alarm
{
    [DisallowMultipleComponent]
    public sealed class AlarmRingingVisualController : MonoBehaviour
    {
        [SerializeField]
        private AlarmRuntimeState runtimeState;

        [SerializeField]
        private Transform alarmVisual;

        [SerializeField]
        private float rotationSpeed = 45f;

        [SerializeField]
        private float pulseSpeed = 4f;

        [SerializeField]
        private float pulseAmount = 0.04f;

        private Vector3 initialScale;
        private bool isRinging;

        private void Awake()
        {
            if (runtimeState == null)
            {
                runtimeState =
                    FindFirstObjectByType<AlarmRuntimeState>();
            }

            if (alarmVisual != null)
            {
                initialScale = alarmVisual.localScale;
            }
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

            isRinging = false;
            ResetVisual();
        }

        private void Update()
        {
            if (!isRinging || alarmVisual == null)
            {
                return;
            }

            alarmVisual.Rotate(
                Vector3.up,
                rotationSpeed * Time.deltaTime,
                Space.Self);

            float pulse =
                1f +
                Mathf.Sin(
                    Time.time * pulseSpeed) *
                pulseAmount;

            alarmVisual.localScale =
                initialScale * pulse;
        }

        private void HandleRingingStateChanged(
            bool ringing)
        {
            isRinging = ringing;

            if (!ringing)
            {
                ResetVisual();
            }
        }

        private void ResetVisual()
        {
            if (alarmVisual == null)
            {
                return;
            }

            alarmVisual.localScale = initialScale;
        }
    }
}
