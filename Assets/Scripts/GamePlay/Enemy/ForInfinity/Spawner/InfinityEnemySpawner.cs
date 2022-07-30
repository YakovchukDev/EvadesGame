using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Enemy.ForInfinity.Spawner
{
    public class InfinityEnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _enemyClass1;
        [SerializeField] private List<GameObject> _enemyClass2;
        [SerializeField] private List<GameObject> _enemyClass3;
        [SerializeField] private List<GameObject> _enemyClass4;
        [SerializeField] private List<GameObject> _allEnemy;
        [SerializeField] private List<Animation> _animation;
        [SerializeField] private ParticleSystem _enemyDieParticle;
        private readonly Vector3[] _spawnPosition = new Vector3[4];
        private readonly Vector3[] _spawnRotation = new Vector3[4];
        public static int SpawnNumber;
        public static int Index;
        private float _spawnTime = 3;

        private void Start()
        {
            _spawnPosition[0] = new Vector3(22, 2, 0);
            _spawnPosition[1] = new Vector3(-22, 2, 0);
            _spawnPosition[2] = new Vector3(0, 2, 22);
            _spawnPosition[3] = new Vector3(0, 2, -22);

            _spawnRotation[0] = new Vector3(0, -90, 0);
            _spawnRotation[1] = new Vector3(0, 90, 0);
            _spawnRotation[2] = new Vector3(0, 180, 0);
            _spawnRotation[3] = new Vector3(0, 0, 0);

            StartCoroutine(EnemySpawner());
        }


        private IEnumerator EnemySpawner()
        {
            while (SpawnNumber < 40)
            {
                Index = Random.Range(0, 4);

                if (SpawnNumber < 5)
                {
                    _animation[Index].Play("SlidingDoors");
                    SpawnNumber++;
                    _enemyClass1[0].transform.localScale = new Vector3(40f, 40f, 40f);
                    _allEnemy.Add(Instantiate(_enemyClass1[0], _spawnPosition[Index],
                        Quaternion.Euler(_spawnRotation[Index])));
                    yield return new WaitForSeconds(1);
                }
                else if (SpawnNumber < 25)
                {
                    Spawn(_enemyClass1);
                    yield return new WaitForSeconds(_spawnTime += 0.2f);
                }
                else if (SpawnNumber < 30)
                {
                    Destroy();
                    Spawn(_enemyClass2);
                    yield return new WaitForSeconds(_spawnTime);
                }
                else if (SpawnNumber < 35)
                {
                    Destroy();
                    Spawn(_enemyClass3);
                    yield return new WaitForSeconds(_spawnTime);
                }
                else if (SpawnNumber < 40)
                {
                    Destroy();
                    Spawn(_enemyClass4);
                    yield return new WaitForSeconds(_spawnTime);
                }
                else
                {
                    SpawnNumber++;
                    Debug.LogError(SpawnNumber);
                }
            }
        }

        private void Destroy()
        {
            _enemyDieParticle.transform.localScale = _allEnemy[0].transform.localScale / 480;
            Instantiate(_enemyDieParticle, _allEnemy[0].transform.position, Quaternion.identity);
            Destroy(_allEnemy[0]);
            _allEnemy.Remove(_allEnemy[0]);
        }

        private void Spawn(List<GameObject> enemyClass)
        {
            _animation[Index].Play("SlidingDoors");
            SpawnNumber++;
            GameObject enemy = enemyClass[Random.Range(0, enemyClass.Count)];
            float enemySize = Random.Range(30f, 100f);
            enemy.transform.localScale = new Vector3(enemySize, enemySize, enemySize);
            _allEnemy.Add(Instantiate(enemy, _spawnPosition[Index], Quaternion.Euler(_spawnRotation[Index])));
        }
    }
}