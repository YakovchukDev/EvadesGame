using UnityEngine;

namespace GamePlay.Enemy.Skill
{
    public class GrowUpDown : MonoBehaviour
    {
        private bool _upDown = true;
        private float _maxScale;
        private float _minScale;
        private float _changeScale;
        private float _timer = 3;
        private bool _timerGo;

        void Start()
        {
            var localScale = transform.GetChild(0).localScale;
            localScale = new Vector3
                (localScale.x / 1.5f, localScale.y / 1.5f, localScale.z / 1.5f);
            transform.GetChild(0).localScale = localScale;
            _minScale = localScale.x;
            _changeScale = localScale.x;
            _maxScale = _minScale * 2;
        }

        private void Update()
        {
            IncreaseDecrease();
            if (_timerGo)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _timer = 3;
                    _maxScale *= 2;
                    _minScale *= 2;
                    _changeScale *= 2;
                    transform.GetChild(0).localScale = new Vector3(_changeScale, _changeScale, _changeScale);
                    _timerGo = false;
                }
            }
        }

        private void IncreaseDecrease()
        {
            if (_upDown)
            {
                if (_changeScale <= _maxScale)
                {
                    _changeScale += 0.3f * Time.deltaTime;
                    transform.GetChild(0).localScale = new Vector3(_changeScale, _changeScale, _changeScale);
                    
                }
                else
                    _upDown = false;
            }
            else
            {
                if (_changeScale >= _minScale)
                {
                    _changeScale -= 0.3f * Time.deltaTime;
                    transform.GetChild(0).localScale = new Vector3(_changeScale, _changeScale, _changeScale);
                }
                else
                    _upDown = true;
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Minimize"))
            {
                ForScale();
            }
        }

        private void ForScale()
        {
            if (!_timerGo)
            {
                _maxScale /= 2;
                _minScale /= 2;
                _changeScale /= 2;
                _timerGo = true;
            }
        }
    }
}