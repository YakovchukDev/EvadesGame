using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Character.Spell.Morfe
{
    public class ActiveSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private List<GameObject> _minimizePatrons;
        [SerializeField] private List<GameObject> _deactivatingPatrons;
        [SerializeField] private GameObject _minimizePatron;
        [SerializeField] private GameObject _deactivatingPatron;
        [SerializeField] private float _manaCostMinimize;
        [SerializeField] private float _manaCostDeactivating;
        private float _minimizeRotation = -12.5f;
        private float _deactivatingRotation = -12.5f;

        private void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                _minimizePatrons.Add(Instantiate(_minimizePatron, transform.position, transform.rotation));
                _minimizePatrons[i].SetActive(false);
            }

            for (int i = 0; i < 5; i++)
            {
                _deactivatingPatrons.Add(Instantiate(_deactivatingPatron, transform.position, transform.rotation));
                _deactivatingPatrons[i].SetActive(false);
            }
        }

        public void MinimizeSpell()
        {
            if (_reloadSpell._canUseSpellFirst)
            {
                _reloadSpell.ReloadFirstSpell(3);
                if (_manaController.ManaReduction(_manaCostMinimize))
                {
                    _minimizeRotation = -12.5f;
                    foreach (var minimizePatrons in _minimizePatrons)
                    {
                        minimizePatrons.transform.rotation = Quaternion.identity;
                    }

                    foreach (var minimizePatrons in _minimizePatrons)
                    {
                        minimizePatrons.transform.position = transform.position;
                        minimizePatrons.transform.rotation = transform.rotation;
                        minimizePatrons.transform.Rotate(0, _minimizeRotation += 5, 0);
                        minimizePatrons.SetActive(true);
                    }
                }
            }
        }

        public void DeactivatingSpell()
        {
            if (_reloadSpell._canUseSpellSecond)
            {
                _reloadSpell.ReloadSecondSpell(3);
                if (_manaController.ManaReduction(_manaCostDeactivating))
                {
                    _deactivatingRotation = -12.5f;
                    foreach (var deactivatingPatrons in _deactivatingPatrons)
                    {
                        deactivatingPatrons.transform.rotation = Quaternion.identity;
                    }

                    foreach (var deactivatingPatrons in _deactivatingPatrons)
                    {
                        deactivatingPatrons.transform.position = transform.position;
                        deactivatingPatrons.transform.rotation = transform.rotation;

                        deactivatingPatrons.transform.Rotate(0, _deactivatingRotation += 5, 0);
                        deactivatingPatrons.SetActive(true);
                    }
                }
            }
        }
    }
}