using UnityEngine;

namespace GamePlay.Character.Spell.TelepFreeze
{
    public class FreezeSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;

        [SerializeField] private GameObject _freezField;
        [SerializeField] private GameObject _gravityRadius;

        private float _timeExistenceAfterMaxSize;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _manaCost;
        private float _size;
        private bool _goFreez;

        private void Start()
        {
            _freezField.SetActive(false);
        }

        private void Update()
        {
            if (_goFreez)
            {
                OnFreezField(_maxSize);
            }
        }

        public void FreezField()
        {
            if (_goFreez == false)
            {
                if (_reloadSpell._canUseSpellFirst)
                {
                    _reloadSpell.ReloadFirstSpell(4);
                    if (_manaController.ManaReduction(_manaCost))
                        _goFreez = true;
                }
            }
        }

        private void OnFreezField(float maxSize)
        {
            _freezField.SetActive(true);
            _gravityRadius.SetActive(false);
            _size += 4 * Time.deltaTime;
            _freezField.transform.localScale = new Vector3(_size, transform.localScale.y, _size);
            if (_size >= maxSize)
            {
                _size = maxSize;
                _timeExistenceAfterMaxSize += Time.deltaTime;
                if (_timeExistenceAfterMaxSize >= 1)
                {
                    _freezField.SetActive(false);
                    _gravityRadius.SetActive(true);

                    _size = 1;
                    _freezField.transform.localScale = new Vector3(_size, transform.localScale.y, _size);
                    _goFreez = false;
                    _timeExistenceAfterMaxSize = 0;
                }
            }
        }
    }
}