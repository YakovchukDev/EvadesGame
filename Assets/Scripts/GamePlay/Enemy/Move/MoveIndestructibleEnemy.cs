using GamePlay.Enemy.Spawner;
using UnityEngine;

namespace GamePlay.Enemy.Move
{
    public class MoveIndestructibleEnemy : MonoBehaviour
    {
        [SerializeField] private float _z;
        [SerializeField] private float _x;
        private float helpY = 90;
        private float y = 0;
        private float _speed=0.01f;
        private float _time;


        private void FixedUpdate()
        {
            transform.localScale= new Vector3(2,2,2);
            if (InfinityEnemySpawner.SpawnNumber >= 40)
            {
                _time += Time.deltaTime;
                if (_time > 5)
                {
                    _time = 0;
                    _speed += 0.0005f;
                }
                    
                MoveSystem(_speed);
            }
            else
            {
                MoveSystem(0.01f);
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                DirectioAndSpeedMovement();
            }
        }
        private void MoveSystem(float speed)
        {
            transform.Translate(new Vector3(_x * speed, 0, _z * speed));
        }
        private void DirectioAndSpeedMovement()
        {
            transform.Rotate(0, y + helpY, 0);
        }
    }
}