using GamePlay.Enemy.Spawner;
using UnityEngine;

namespace GamePlay.Enemy.Move
{
    public class MoveEnemy : MonoBehaviour
    {
        [SerializeField] protected GameObject _freezeField;
        private float _freezeTimer;
        protected bool CanFreeze;
        [SerializeField] private float _speed = 5;
        private float _time;
        [SerializeField] private ParticleSystem _enemyBounceParticle;
        private float _rotationSpeed=20;

        private void FixedUpdate()
        {

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
                Quaternion rotation = Quaternion.LookRotation(reflectDir);
                transform.eulerAngles = new Vector3(0, rotation.eulerAngles.y, 0);
                
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
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
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