using UnityEngine;

namespace GamePlay.Character.Spell.Morfe
{
    public class MinimizeSpell : MonoBehaviour
    {
        private Vector3 _direction;
        private float _timer = 2;
        private bool _applyDirection;

        private void Start()
        {
            transform.parent = null;
            _applyDirection = true;
            if (_applyDirection)
            {
                _direction =new Vector3(0,0,0.4f); 
                _applyDirection = false;
            }
        }
        private void Update()
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
            if (gameObject.activeSelf)
            {
                if (_direction != new Vector3(0, 0, 0))
                {
                    gameObject.transform.Translate(_direction);
                }
            }
            _timer -= Time.deltaTime;
            if (_timer <= 0)
            {
                ReturnToPool();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                ReturnToPool();
            }
        }

        private void ReturnToPool()
        {
            _timer = 2;
            _applyDirection = true;
            gameObject.SetActive(false);
            transform.localPosition = Vector3.zero;
        }
    }
}