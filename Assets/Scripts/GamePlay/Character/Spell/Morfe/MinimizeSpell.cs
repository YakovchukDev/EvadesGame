using UnityEngine;

namespace GamePlay.Character.Spell.Morfe
{
    public class MinimizeSpell : MonoBehaviour
    {
       // [SerializeField] private GameObject _character;

        private Vector3 _direction;
        private VariableJoystick _variableJoystick;
        private float _timer = 2;
        private bool _applyDirection;

        private void Start()
        {
            transform.parent = null;

            _variableJoystick = FindObjectOfType<VariableJoystick>();

            _applyDirection = true;
            if (_applyDirection)
            {
                _direction =new Vector3(0,0,0.18f); 
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
                //Destroy(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy"))
            {
                other.gameObject.transform.localScale /= 2;
            }

            if (other.gameObject.CompareTag("WallX"))
            {
                ReturnToPool();
            }

            if (other.gameObject.CompareTag("WallZ"))
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