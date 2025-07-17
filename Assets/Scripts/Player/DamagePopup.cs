using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace TwoDotFiveDimension
{
    public class DamagePopup : MonoBehaviour
    {
        [FormerlySerializedAs("speed")]
        [Header("Popup Settings")]
        [SerializeField] private float _speed = 8f;
        [SerializeField] private float _upwardFactor = 0.5f;
        [SerializeField] private float _lifeTime = 1f;
        [SerializeField] private float _fadeSpeed = 5f;
        [SerializeField] private float _growRate = 1f;

        [Header("Colors")]
        [SerializeField] private string _normalHex = "#FFB600";
        [SerializeField] private string _criticalHex = "#FF0000";

        private static int _sortingOrder;
        private TextMeshPro _textMeshPro;
        private Color _textColor;
        private float _timer;
        private Vector3 _velocity;

        public static DamagePopup Create(Transform prefab, Vector3 position, Vector3 attackDir, int damage, bool isCritical)
        {
            var inst = Instantiate(prefab, position, Quaternion.identity);
            var popup = inst.GetComponent<DamagePopup>();
            popup.Initialize(damage, isCritical, attackDir);
            return popup;
        }

        private void Awake()
        {
            _textMeshPro = GetComponent<TextMeshPro>();
        }

        private void Initialize(int damage, bool isCritical, Vector3 attackDir)
        {
            // Set text & sorting
            _textMeshPro.SetText(damage.ToString());
            _textMeshPro.sortingOrder = ++_sortingOrder;

            // Font size & color
            _textMeshPro.fontSize = isCritical ? 3f : 2f;
            ColorUtility.TryParseHtmlString(
                isCritical ? _criticalHex : _normalHex,
                out _textColor
            );
            _textMeshPro.color = _textColor;

            // Motion & lifetime
            _velocity = attackDir.normalized * _speed + Vector3.up * (_speed * _upwardFactor);
            _timer = _lifeTime;
        }

        private void Update()
        {
            // Movement (with damping)
            transform.position += _velocity * Time.deltaTime;
            _velocity *= 1f - (_speed * Time.deltaTime / _lifeTime);

            
            // Scale in–out: membesar lalu mengecil
            float scaleStep = _growRate * Time.deltaTime;
            if (_timer > _lifeTime * 0.5f)
                transform.localScale += Vector3.one * scaleStep;  // membesar
            else
                transform.localScale -= Vector3.one * scaleStep;  // mengecil
            
            // Lifetime & fade
            _timer -= Time.deltaTime;
            if (_timer <= 0f)
            {
                _textColor.a -= _fadeSpeed * Time.deltaTime;
                _textMeshPro.color = _textColor;
                if (_textColor.a <= 0f)
                    Destroy(gameObject);
            }
        }
    }
}
