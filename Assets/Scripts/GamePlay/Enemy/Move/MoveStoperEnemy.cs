using GamePlay.Enemy.Spawner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay.Enemy.Move
{
    public class MoveStoperEnemy : MoveEnemy
    {
        [SerializeField] private ParticleSystem _moveParticle;
        private Quaternion _rotation;
        private float _timeForMove;
        private float _speed = 5;
        private float _time;
        private const float RotationSpeed = 15;
        private float _startLifetimeParticle;
        private bool _rotate = true;
        private GameObject _gameObject;

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "MapGeneratorBeta")
            {
                GetComponent<MoveStoperEnemy>().enabled = true;
            }
        }

        private void Start()
        {
            _gameObject = GameObject.FindGameObjectWithTag("Freeze");
            _rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
            _startLifetimeParticle = _moveParticle.startLifetime;
        }

        private void FixedUpdate()
        {
            RotateController();
            _freezeField = _gameObject;

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
                    MoveSystem(5);
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

        private void MoveSystem(float speed)
        {
            _timeForMove += Time.deltaTime;
            if (_timeForMove <= 3.5)
            {
                transform.Translate(Vector3.forward * (Time.deltaTime * speed));
                _moveParticle.startLifetime = _startLifetimeParticle;
            }
            else if (_timeForMove >= 7)
            {
                _timeForMove = 0;
            }
            else
            {
                _moveParticle.startLifetime *= 0;
            }
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
    }
}