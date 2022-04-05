using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character.Spell
{
    public class ReloadSpell : MonoBehaviour
    {
        [SerializeField] private List<Image> _firstImage;
        [SerializeField] private List<Image> _secondImage;
        private float _timeReloadFirst;
        private float _timeReloadSecond;
        public bool _canUseSpellFirst = true;
        public bool _canUseSpellSecond = true;

        private void Update()
        {
            if (_canUseSpellFirst == false)
            {
                foreach (var firstImage in _firstImage)
                {
                    firstImage.fillAmount -= 1.0f / _timeReloadFirst * Time.deltaTime / 2.2f;
                }

                _timeReloadFirst -= Time.deltaTime;
                if (_timeReloadFirst <= 0)
                {
                    _canUseSpellFirst = true;
                    foreach (var firstImage in _firstImage)
                    {
                        firstImage.fillAmount = 1;
                    }
                }
            }

            if (_canUseSpellSecond == false)
            {
                foreach (var secondImage in _secondImage)
                {
                    secondImage.fillAmount -= 1.0f / _timeReloadSecond * Time.deltaTime / 2.2f;
                }

                _timeReloadSecond -= Time.deltaTime;
                if (_timeReloadSecond <= 0)
                {
                    _canUseSpellSecond = true;
                    foreach (var secondImage in _secondImage)
                    {
                        secondImage.fillAmount = 1;
                    }
                }
            }
        }

        public void ReloadFirstSpell(float timeToReload)
        {
            _canUseSpellFirst = false;
            _timeReloadFirst = timeToReload;
        }

        public void ReloadSecondSpell(float timeToReload)
        {
            _canUseSpellSecond = false;
            _timeReloadSecond = timeToReload;
        }
    }
}