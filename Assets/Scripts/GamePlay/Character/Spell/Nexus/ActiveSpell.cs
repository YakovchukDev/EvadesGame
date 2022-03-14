using System.Collections.Generic;
using Joystick_Pack.Examples;
using UnityEngine;

namespace GamePlay.Character.Spell.Nexus
{
    public class ActiveSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private float _manaCost;
        [SerializeField] private GameObject _invulnerableField;

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

        private void Respawn()
        {
        }
    }
}