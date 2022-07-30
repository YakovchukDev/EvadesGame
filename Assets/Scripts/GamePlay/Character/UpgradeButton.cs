using UnityEngine;
using TMPro;
using UnityEngine.UI;

namespace GamePlay.Character
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private UpgradeButtonEnum _type;
        [SerializeField] private TMP_Text _amountText;
        [SerializeField] private Image _imageButton;
        [SerializeField] private bool _isNeedMana;

        public UpgradeButtonEnum Type => _type;
        public bool IsNeedMana => _isNeedMana;
        public TMP_Text GetText => _amountText;
        public Image ImageButton => _imageButton;

    }
}
