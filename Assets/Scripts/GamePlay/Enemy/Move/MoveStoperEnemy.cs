using GamePlay.Enemy.Spawner;
using UnityEngine;

namespace GamePlay.Enemy.Move
{
    public class MoveStoperEnemy : MoveEnemy
    {
        private float _timeForMove;
        private float _speed = 5;
        private float _time;
        private float _rotationSpeed = 15;
        private Quaternion _rotation;
        private bool _rotate = true;

        private void Start()
        {
            _rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
        }

        private void FixedUpdate()
        {
            if (_rotate)
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, _rotationSpeed * Time.deltaTime);
                if (transform.rotation == _rotation)
                {
                    _rotate = false;
                }
            }

            _freezeField = GameObject.FindGameObjectWithTag("Freeze");

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
                _rotation = Quaternion.LookRotation(reflectDir);
                _rotate = true;
                foreach (ContactPoint missileHit in other.contacts)
                {
                    Vector3 hitPoint = missileHit.point;
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
                transform.Translate(Vector3.forward * Time.deltaTime * speed);
            }
            else if (_timeForMove >= 7)
            {
                _timeForMove = 0;
            }
        }
    }
}