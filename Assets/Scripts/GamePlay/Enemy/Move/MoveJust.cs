using GamePlay.Enemy.ForInfinity.Spawner;
using UnityEngine;
using Random = UnityEngine.Random;
using UnityEngine.SceneManagement;

namespace GamePlay.Enemy.Move
{
    public class MoveJust : EnemyMove
    {
        private void Awake()
        {
            if (SceneManager.GetActiveScene().name == "Company")
            {
                GetComponent<MoveJust>().enabled = true;
            }
        }

        private void Start()
        {
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
                WallTouch(other);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Freeze"))
            {
                FreezeTimer = 0;
                CanFreeze = true;
            }
        }


        protected override void MoveSystem(float speed)
        {
            RotateController();
            if (CanFreeze == false)
            {
                if (InfinityEnemySpawner.SpawnNumber >= 40)
                {
                    FasterTime += Time.deltaTime;
                    if (FasterTime > 5)
                    {
                        FasterTime = 0;
                        _speed += 0.05f;
                    }
                }

                transform.Translate(Vector3.forward * (Time.deltaTime * speed));
            }
            else
            {
                FreezeMe(TimeForFreeze);
            }
        }
    }
}