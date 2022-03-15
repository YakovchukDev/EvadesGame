using GamePlay.Enemy.Move;
using GamePlay.Enemy.Spawner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay.Enemy
{
    public class StartOnInfinity : MonoBehaviour
    {
        [SerializeField] private int _index;
        Vector3 _targetPoint;
        private float _speed;
        private bool _worked;

        private void Start()
        {
            if (SceneManager.GetActiveScene().name == "InfinityGame")
            {
                _worked = true;
                _speed = 0.05f;
                var moveScript = gameObject.GetComponent<MoveEnemy>();
                moveScript.enabled = false;

                if (InfinityEnemySpawner.Index == 0)
                {
                    _index = 1;
                }

                if (InfinityEnemySpawner.Index == 1)
                {
                    _index = 2;
                }

                if (InfinityEnemySpawner.Index == 2)
                {
                    _index = 3;
                }

                if (InfinityEnemySpawner.Index == 3)
                {
                    _index = 4;
                }
            }
            else
            {
                Destroy(GetComponent<StartOnInfinity>());
            }
        }

        private void FixedUpdate()
        {
            if (_worked)
            {
                var position = transform.position;

                if (_index == 1)
                {
                    _targetPoint = new Vector3(15, position.y, position.z);
                    transform.rotation = Quaternion.Euler(0, -90, 0);
                }

                if (_index == 2)
                {
                    _targetPoint = new Vector3(-15, position.y, position.z);
                    transform.rotation = Quaternion.Euler(0, 90, 0);
                }

                if (_index == 3)
                {
                    _targetPoint = new Vector3(position.x, position.y, 15);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }

                if (_index == 4)
                {
                    _targetPoint = new Vector3(position.x, position.y, -15);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }

                var moveScript = gameObject.GetComponent<MoveEnemy>();
                if (_index == 1)
                {
                    if (transform.position != _targetPoint && _worked)
                    {
                        MoveObj();
                    }
                    else if (transform.position.x == 15)
                    {
                        _worked = false;
                        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                        moveScript.enabled = true;
                    }
                }

                else if (_index == 2)
                {
                    if (transform.position != _targetPoint && _worked)
                    {
                        MoveObj();
                    }
                    else if (transform.position.x == -15)
                    {
                        _worked = false;
                        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                        moveScript.enabled = true;
                    }
                }

                else if (_index == 3)
                {
                    if (transform.position != _targetPoint && _worked)
                    {
                        MoveObj();
                    }
                    else if (transform.position.z == 15)
                    {
                        _worked = false;
                        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                        moveScript.enabled = true;
                    }
                }

                else if (_index == 4)
                {
                    if (transform.position != _targetPoint && _worked)
                    {
                        MoveObj();
                    }
                    else if (transform.position.z == -15)
                    {
                        _worked = false;
                        transform.rotation = Quaternion.Euler(0, Random.Range(0, 360), 0);
                        moveScript.enabled = true;
                    }
                }
            }
        }

        private void MoveObj()
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPoint, _speed);
        }
    }
}