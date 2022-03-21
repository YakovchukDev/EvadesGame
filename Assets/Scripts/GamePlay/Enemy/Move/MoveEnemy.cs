using GamePlay.Enemy.Spawner;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Enemy.Move
{
    public class MoveEnemy : MonoBehaviour
    {
        [SerializeField] protected GameObject _freezeField;
        [SerializeField] protected ParticleSystem _enemyBounceParticle;
        [SerializeField] private float _speed = 5;
        private Quaternion _rotation;
        private GameObject _gameObject;
        private float _freezeTimer;
        private float _time;
        private const float RotationSpeed = 15;
        private bool _rotate;
        protected bool CanFreeze;


        private void Start()
        {
            _gameObject = GameObject.FindGameObjectWithTag("Freeze");
            _rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        }

        private void FixedUpdate()
        {
            _freezeField = _gameObject;
            RotateController();
            if (CanFreeze == false)
            {
                if (InfinityEnemySpawner.SpawnNumber >= 40)
                {
                    _time += Time.deltaTime;
                    if (_time > 5)
                    {
                        _time = 0;
                        _speed += 0.05f;
                    }

                    MoveSystem(_speed);
                }
                else
                {
                    MoveSystem(_speed);
                }
            }
            else
            {
                FreezeMe(3);
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                var reflectDir = Vector3.Reflect(transform.forward, other.GetContact(0).normal);
                _rotation = Quaternion.LookRotation(reflectDir);
                _rotate = true;
                foreach (var missileHit in other.contacts)
                {
                    var hitPoint = missileHit.point;
                    _enemyBounceParticle.transform.localScale = transform.localScale / 30;
                    Instantiate(_enemyBounceParticle, new Vector3(hitPoint.x, hitPoint.y, hitPoint.z),
                        Quaternion.identity);
                }
            }
        }


        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Freeze"))
            {
                _freezeTimer = 0;
                CanFreeze = true;
            }
        }


        private void MoveSystem(float speed)
        {
            transform.Translate(Vector3.forward * (Time.deltaTime * speed));
        }

        private void RotateController()
        {
            if (_rotate)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, RotationSpeed * Time.deltaTime);
                if (transform.rotation == _rotation)
                {
                    _rotate = false;
                }
            }
        }

        protected void FreezeMe(float allFreezeTime)
        {
            MoveSystem(0);
            _freezeTimer += Time.deltaTime;
            if (_freezeTimer >= allFreezeTime)
            {
                CanFreeze = false;
            }
        }
    }
}