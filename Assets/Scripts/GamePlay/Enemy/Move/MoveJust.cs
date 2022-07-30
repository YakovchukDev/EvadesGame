using GamePlay.Enemy.ForInfinity.Spawner;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace GamePlay.Enemy.Move
{
    public class MoveJust : EnemyMove
    {
        private GameObject _bounceParticle;

        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "Company")
            {
                GetComponent<MoveJust>().enabled = true;
            }
        }

        private void Start()
        {
            _bounceParticle = Instantiate(_enemyBounceParticle, transform.position, Quaternion.identity);
            _bounceParticle.SetActive(false);
            Rotation = Quaternion.Euler(0,
                Random.Range(PossiblesRotation1[Random.Range(0, 4)], PossiblesRotation2[Random.Range(0, 4)]), 0);
        }

        private void FixedUpdate()
        {
            MoveSystem(_speed);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                WallTouch(other, _bounceParticle);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Freeze"))
            {
                FreezeTimer = 0;
                CanFreeze = true;
                print(2);
            }
        }


        protected override void MoveSystem(float speed)
        {
            if (CanFreeze == false)
            {
                if (InfinityEnemySpawner.SpawnNumber >= 30)
                {
                    FasterTime += Time.deltaTime;
                    if (FasterTime > 5)
                    {
                        FasterTime = 0;
                        _speed += 0.05f;
                    }
                }

                transform.Translate(Vector3.forward * (Time.deltaTime * speed));
                RotateController();
            }
            else
            {
                FreezeMe(TimeForFreeze);
            }
        }
    }
}