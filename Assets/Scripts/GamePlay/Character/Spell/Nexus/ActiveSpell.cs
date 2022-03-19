using UnityEngine;

namespace GamePlay.Character.Spell.Nexus
{
    public class ActiveSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private GameObject _invulnerableField;
        [SerializeField] private float _manaCost;

        public void InvulnerableField()
        {
            if (_reloadSpell._canUseSpellSecond)
            {
                _reloadSpell.ReloadSecondSpell(3);
                if (_manaController.ManaReduction(_manaCost))
                {
                    _invulnerableField.transform.position = transform.position;
                    _invulnerableField.SetActive(true);
                }
            }
        }
    }
}