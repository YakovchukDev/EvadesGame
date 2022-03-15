using UnityEngine;

namespace Character
{
    public class FreezeSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;

        [SerializeField] private GameObject _freezField;

        private float _size = 0;
        private bool _goFreez = false;
        private float _timeAfterMaxSize = 0;
        private void Start()
        {
            _freezField.SetActive(false);
        }

        private void Update()
        {
            if (_goFreez == true)
            {
                OnFreezField(4);
            }
        }
        public void FreezField()
        {
            if (_goFreez == false)
            {
                if (_manaController.ManaReduction(10f))
                    _goFreez = true;
            }
        }
        private void OnFreezField(float maxSize)
        {

            _freezField.SetActive(true);

            _size += 4 * Time.deltaTime;
            _freezField.transform.localScale = new Vector3(_size, transform.localScale.y, _size);
            if (_size >= maxSize)
            {
                _size = maxSize;
                _timeAfterMaxSize += Time.deltaTime;
                if (_timeAfterMaxSize >= 1)
                {
                    _freezField.SetActive(false);
                    _size = 1;
                    _freezField.transform.localScale = new Vector3(_size, transform.localScale.y, _size);
                    _goFreez = false;
                    _timeAfterMaxSize = 0;
                }

            }
        }
    }
}