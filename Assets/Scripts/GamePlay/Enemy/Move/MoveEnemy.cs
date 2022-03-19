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
        private Quaternion Rotation { get; set; }
        private float _freezeTimer;
        private float _time;
        private float _rotationSpeed=15;
        private bool Rotate { get; set; }
        protected bool CanFreeze;
        private GameObject _gameObject;


        private void Start()
        {
            _gameObject = GameObject.FindGameObjectWithTag("Freeze");
            Rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        }

        private void FixedUpdate()
        {
            
            if (Rotate)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, Rotation,_rotationSpeed * Time.deltaTime);
                if (transform.rotation == Rotation)
                {
                    Rotate = false;
                }
            }
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
                Vector3 reflectDir = Vector3.Reflect(transform.forward, other.GetContact(0).normal);
                Rotation = Quaternion.LookRotation(reflectDir);
                Rotate = true;
                foreach (ContactPoint missileHit in other.contacts)
                {
                    Vector3 hitPoint = missileHit.point;
                    _enemyBounceParticle.transform.localScale = transform.localScale/30;
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