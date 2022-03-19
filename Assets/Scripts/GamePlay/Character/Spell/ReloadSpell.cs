using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character.Spell
{
    public class ReloadSpell : MonoBehaviour
    {
        [SerializeField] private Image _firstImage;
        [SerializeField] private Image _secondImage;
        private float _timeReloadFirst;
        private float _timeReloadSecond;
        public bool _canUseSpellFirst = true;
        public bool _canUseSpellSecond = true;
        private void Update()
        {
            if (_canUseSpellFirst == false)
            {
                _firstImage.fillAmount -= 1.0f / _timeReloadFirst * Time.deltaTime / 2.2f;
                _timeReloadFirst -= Time.deltaTime;
                if (_timeReloadFirst <= 0)
                {
                    _canUseSpellFirst = true;
                    _firstImage.fillAmount = 1;
                }
            }

            if (_canUseSpellSecond == false)
            {
                _secondImage.fillAmount -= 1.0f / _timeReloadSecond * Time.deltaTime / 2.2f;
                _timeReloadSecond -= Time.deltaTime;
                if (_timeReloadSecond <= 0)
                {
                    _canUseSpellSecond = true;
                    _secondImage.fillAmount = 1;
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