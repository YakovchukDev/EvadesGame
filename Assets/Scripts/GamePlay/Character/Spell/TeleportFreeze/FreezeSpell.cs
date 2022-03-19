using UnityEngine;

namespace GamePlay.Character.Spell.TeleportFreeze
{
    public class FreezeSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        [SerializeField] private GameObject _freezeField;
        [SerializeField] private GameObject _gravityRadius;
        [SerializeField] private float _maxSize;
        [SerializeField] private float _manaCost;
        private float _timeExistenceAfterMaxSize;
        private float _size;
        private bool _goFreeze;

        private void Start()
        {
            _freezeField.SetActive(false);
        }

        private void Update()
        {
            if (_goFreeze)
            {
                OnFreezeField(_maxSize);
            }
        }

        public void FreezeField()
        {
            if (_goFreeze == false)
            {
                if (_reloadSpell._canUseSpellFirst)
                {
                    _reloadSpell.ReloadFirstSpell(4);
                    if (_manaController.ManaReduction(_manaCost))
                        _goFreeze = true;
                }
            }
        }

        private void OnFreezeField(float maxSize)
        {
            _freezeField.SetActive(true);
            _gravityRadius.SetActive(false);
            _size += 4 * Time.deltaTime;
            _freezeField.transform.localScale = new Vector3(_size, transform.localScale.y, _size);
            if (_size >= maxSize)
            {
                _size = maxSize;
                _timeExistenceAfterMaxSize += Time.deltaTime;
                if (_timeExistenceAfterMaxSize >= 1)
                {
                    _freezeField.SetActive(false);
                    _gravityRadius.SetActive(true);

                    _size = 1;
                    _freezeField.transform.localScale = new Vector3(_size, transform.localScale.y, _size);
                    _goFreeze = false;
                    _timeExistenceAfterMaxSize = 0;
                }
            }
        }
    }
}