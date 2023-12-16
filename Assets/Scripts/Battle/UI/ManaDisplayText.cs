using Battle.DataHolders;
using TMPro;
using UnityEngine;

namespace Battle.UI
{
    public interface IManaDisplay
    {
        void SetUp(IReadOnlyMana mana);
    }


    /// <summary>
    ///     Simple display of mana in text form.
    ///     Can be replaced in the future with more complex UI using interface IManaDisplay.
    /// </summary>
    [RequireComponent(typeof(TMP_Text))]
    public class ManaDisplayText : MonoBehaviour, IManaDisplay
    {
        [SerializeField] private Color manaColor = Color.cyan;
        private IReadOnlyMana _mana;
        private TMP_Text _text;

        private void Awake()
        {
            _text = GetComponent<TMP_Text>();
            _text.color = manaColor;
        }

        public void SetUp(IReadOnlyMana mana)
        {
            _mana = mana;
            _mana.OnManaChanged += UpdateUI;
            UpdateUI();
        }

        private void UpdateUI()
        {
            _text.text = $"{_mana.Current:0} / {_mana.Max:0}";
        }
    }
}