using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UIController.Stats
{
    public class CooldownUI: MonoBehaviour
    {
        [SerializeField] private Image _cooldownOverlay;
        [SerializeField] private TMP_Text _cooldownText;

        private float _cooldownDuration;
        private float _cooldownEndTime;
        private bool _isCoolingDown;

        public void StartCooldown(float duration)
        {
            _cooldownDuration = duration;
            _cooldownEndTime = Time.time + duration;
            _isCoolingDown = true;

            _cooldownOverlay.fillAmount = 1f;
            _cooldownOverlay.enabled = true;

            if (_cooldownText != null)
                _cooldownText.enabled = true;
        }

        private void Update()
        {
            if (!_isCoolingDown) return;

            float timeLeft = _cooldownEndTime - Time.time;
            if (timeLeft <= 0f)
            {
                _isCoolingDown = false;
                _cooldownOverlay.fillAmount = 0f;
                _cooldownOverlay.enabled = false;

                if (_cooldownText != null)
                    _cooldownText.enabled = false;

                return;
            }

            _cooldownOverlay.fillAmount = timeLeft / _cooldownDuration;

            if (_cooldownText != null)
                _cooldownText.text = Mathf.CeilToInt(timeLeft).ToString();
        }
    }
}