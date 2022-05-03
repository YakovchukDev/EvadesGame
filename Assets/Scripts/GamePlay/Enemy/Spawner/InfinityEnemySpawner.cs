using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GamePlay.Enemy.Spawner
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

        private readonly Vector3[] _spawnPos = new Vector3[4];
        private readonly float[] _possibleSizes = {20f, 40f, 100f};
        public static int SpawnNumber;
        public static int Index;
        private float _spawnTime = 2;

        private void Start()
        {
            Index = Random.Range(0, 4);
            _spawnPos[0] = new Vector3(22, 2, 0);
            _spawnPos[1] = new Vector3(-22, 2, 0);
            _spawnPos[2] = new Vector3(0, 2, 22);
            _spawnPos[3] = new Vector3(0, 2, -22);
            StartCoroutine(EnemySpawner());
        }


        IEnumerator EnemySpawner()
        {
            while (SpawnNumber < 40)
            {
                if (SpawnNumber < 5)
                {
                    _animation[Index].Play("SlidingDoors");
                    SpawnNumber++;
                    _enemyClass1[0].transform.localScale = new Vector3(40f, 40f, 40f);
                    _allEnemy.Add(Instantiate(_enemyClass1[0], _spawnPos[Index],
                        Quaternion.Euler(0, Random.Range(0, 360), 0)));
                    yield return new WaitForSeconds(1);
                    Index = Random.Range(0, 4);
                }
                else if (SpawnNumber < 25)
                {
                    _animation[Index].Play("SlidingDoors");
                    SpawnNumber++;
                    GameObject enemy = _enemyClass1[Random.Range(0, _enemyClass1.Count)];
                    float enemySize = _possibleSizes[Random.Range(0, _possibleSizes.Length)];
                    enemy.transform.localScale = new Vector3(enemySize, enemySize, enemySize);
                    _allEnemy.Add(Instantiate(enemy, _spawnPos[Index], Quaternion.Euler(-90, Random.Range(0, 360), 0)));
                    yield return new WaitForSeconds(_spawnTime += 0.2f);

                    Index = Random.Range(0, 4);
                }
                else if (SpawnNumber < 30)
                {
                    _enemyDieParticle.transform.localScale = _allEnemy[0].transform.localScale / 480;
                    Instantiate(_enemyDieParticle, _allEnemy[0].transform.position, Quaternion.identity);
                    Destroy(_allEnemy[0]);
                    _allEnemy.Remove(_allEnemy[0]);
                    _animation[Index].Play("SlidingDoors");
                    SpawnNumber++;
                    GameObject enemy = _enemyClass2[Random.Range(0, _enemyClass2.Count)];
                    float enemySize = _possibleSizes[Random.Range(0, _possibleSizes.Length)];
                    enemy.transform.localScale = new Vector3(enemySize, enemySize, enemySize);
                    _allEnemy.Add(Instantiate(enemy, _spawnPos[Index], Quaternion.Euler(0, Random.Range(0, 360), 0)));
                    yield return new WaitForSeconds(_spawnTime);
                    Index = Random.Range(0, 4);
                }
                else if (SpawnNumber < 35)
                {
                    _enemyDieParticle.transform.localScale = _allEnemy[0].transform.localScale / 480;
                    Instantiate(_enemyDieParticle, _allEnemy[0].transform.position, Quaternion.identity);
                    Destroy(_allEnemy[0]);
                    _allEnemy.Remove(_allEnemy[0]);
                    _animation[Index].Play("SlidingDoors");
                    SpawnNumber++;
                    GameObject enemy = _enemyClass3[Random.Range(0, _enemyClass3.Count)];
                    float enemySize = _possibleSizes[Random.Range(0, _possibleSizes.Length)];
                    enemy.transform.localScale = new Vector3(enemySize, enemySize, enemySize);
                    _allEnemy.Add(Instantiate(enemy, _spawnPos[Index], Quaternion.Euler(0, Random.Range(0, 360), 0)));
                    yield return new WaitForSeconds(_spawnTime);
                    Index = Random.Range(0, 4);
                }
                else if (SpawnNumber < 40)
                {
                    _enemyDieParticle.transform.localScale = _allEnemy[0].transform.localScale / 480;
                    Instantiate(_enemyDieParticle, _allEnemy[0].transform.position, Quaternion.identity);
                    Destroy(_allEnemy[0]);
                    _allEnemy.Remove(_allEnemy[0]);
                    _animation[Index].Play("SlidingDoors");
                    SpawnNumber++;
                    GameObject enemy = _enemyClass4[Random.Range(0, _enemyClass4.Count)];
                    float enemySize = _possibleSizes[Random.Range(0, _possibleSizes.Length)];
                    enemy.transform.localScale = new Vector3(enemySize, enemySize, enemySize);
                    _allEnemy.Add(Instantiate(enemy, _spawnPos[Index], Quaternion.Euler(0, Random.Range(0, 360), 0)));
                    yield return new WaitForSeconds(_spawnTime);
                    Index = Random.Range(0, 4);
                }
                else
                {
                    SpawnNumber++;
                }
            }
        }
    }
}