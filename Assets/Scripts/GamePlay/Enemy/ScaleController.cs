using UnityEngine;

namespace GamePlay.Enemy
{
    public class ScaleController : MonoBehaviour
    {
        private Vector3 _startScale;
        private float _timer=6;

        private void Start()
        {
            _startScale = transform.localScale;
        }

        private void Update()
        {
            ScaleControl();
        }

        private void ScaleControl()
        {
            if (_startScale != transform.localScale)
            {
                _timer -= Time.deltaTime;
                if (_timer <= 0)
                {
                    _timer = 12;
                    transform.localScale = _startScale;
                }
            }
        }
    }
}