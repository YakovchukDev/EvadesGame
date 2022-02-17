using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Enemy.Skill
{
    public class EnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _smallEnemy;
        [SerializeField] private List<GameObject> _mediumEnemy;
        [SerializeField] private List<GameObject> _bigEnemy;
        [SerializeField] private List<GameObject> _BossEnemy;

        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "InfinityGame")
                InfinitySpawner();
            else
                Spawner();
        }
        private void Spawner()
        {
            foreach (var enemy in _smallEnemy)
            {
                Instantiate(enemy, new Vector3(Random.Range(-30.0f, 30.0f), 2, Random.Range(-5.0f, 5.0f)), Quaternion.identity);
                enemy.transform.localScale = new Vector3(0.5F, 0.5F, 0.5F);
            }
            foreach (var enemy in _mediumEnemy)
            {
                Instantiate(enemy, new Vector3(Random.Range(-30.0f, 30.0f), 2, Random.Range(-5.0f, 5.0f)), Quaternion.identity);
                enemy.transform.localScale = new Vector3(1, 1, 1);
            }
            foreach (var enemy in _bigEnemy)
            {
                Instantiate(enemy, new Vector3(Random.Range(-30.0f, 30.0f), 2, Random.Range(-5.0f, 5.0f)), Quaternion.identity);
                enemy.transform.localScale = new Vector3(2, 2, 2);
            }
            foreach (var enemy in _BossEnemy)
            {
                Instantiate(enemy, new Vector3(Random.Range(-30.0f, 30.0f), 2, Random.Range(-5.0f, 5.0f)), Quaternion.identity);
                enemy.transform.localScale = new Vector3(4, 4, 4);
            }
        }
        private void InfinitySpawner()
        {
            foreach (var enemy in _smallEnemy)
            {
                Instantiate(enemy, new Vector3(0, 2, 0), Quaternion.identity);
                enemy.transform.localScale = new Vector3(0.5F, 0.5F, 0.5F);
            }
            foreach (var enemy in _mediumEnemy)
            {
                Instantiate(enemy, new Vector3(0, 2, 0), Quaternion.identity);
                enemy.transform.localScale = new Vector3(1, 1, 1);
            }
            foreach (var enemy in _bigEnemy)
            {
                Instantiate(enemy, new Vector3(0, 2, 0), Quaternion.identity);
                enemy.transform.localScale = new Vector3(2, 2, 2);
            }
            foreach (var enemy in _BossEnemy)
            {
                Instantiate(enemy, new Vector3(0, 2, 0), Quaternion.identity);
                enemy.transform.localScale = new Vector3(4, 4, 4);
            }
        }
    }
}