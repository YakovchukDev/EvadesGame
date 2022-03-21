using UnityEngine;

namespace GamePlay.Character.Spell.Morfe
{
    public class DeactivatingSpell : MonoBehaviour
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
                _direction = new Vector3(0, 0, 0.4f);
                _applyDirection = false;
            }
        }

        private void Update()
        {
            transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
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
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                var materials = other.gameObject.transform.GetChild(0).GetComponent<Renderer>().materials;
                foreach (var material in materials)
                {
                    Color startColor = material.color;
                    material.color = new Color(startColor.r, startColor.g, startColor.b, 0.3f);
                }

                other.gameObject.layer = 12;
            }

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