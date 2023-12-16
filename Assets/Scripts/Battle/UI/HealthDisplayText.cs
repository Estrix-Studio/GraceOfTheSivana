using Battle.DataHolders;
using TMPro;
using UnityEngine;

namespace Battle.UI
{
    public interface IHealthDisplay
    {
        void SetUp(IReadOnlyHealth health);
    }


    /// <summary>
    ///     This class is responsible for displaying the health of a character.
    ///     In current iteration, it is a simple text display.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class HealthDisplayText : MonoBehaviour, IHealthDisplay
    {
        [SerializeField] private Color normalColor = Color.green;

        [Space] [SerializeField] private float lowHealthThreshold = 0.4f;

        [SerializeField] private Color lowColor = Color.yellow;

        [Space] [SerializeField] private float criticalHealthThreshold = 0.15f;

        [SerializeField] private Color criticalColor = Color.red;

        [Space] [SerializeField] private Color deadColor = Color.black;

        private IReadOnlyHealth _health;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
        }

        public void SetUp(IReadOnlyHealth health)
        {
            _health = health;
            _health.OnHealthChanged += UpdateUI;
            _health.OnDeath += UpdateUI;
            UpdateUI();
        }

        private void UpdateUI()
        {
            _text.text = $"{_health.Current:000} / {_health.Max:000}";

            if (!_health.IsAlive)
            {
                _text.color = Color.black;
                return;
            }

            var percentage = _health.Current / _health.Max;
            if (percentage <= criticalHealthThreshold)
                _text.color = criticalColor;
            else if (percentage <= lowHealthThreshold)
                _text.color = lowColor;
            else
                _text.color = normalColor;
        }
    }
}