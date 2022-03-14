using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Enemy.Spawner
{
    public class StartEnemySpawner : MonoBehaviour
    {
        [SerializeField] private List<GameObject> _smallEnemy;
        [SerializeField] private List<GameObject> _mediumEnemy;
        [SerializeField] private List<GameObject> _bigEnemy;
        [SerializeField] private List<GameObject> _bossEnemy;

        private void Start()
        {
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
            foreach (var enemy in _bossEnemy)
            {
                Instantiate(enemy, new Vector3(Random.Range(-30.0f, 30.0f), 2, Random.Range(-5.0f, 5.0f)), Quaternion.identity);
                enemy.transform.localScale = new Vector3(4, 4, 4);
            }
        }
       
    }
}