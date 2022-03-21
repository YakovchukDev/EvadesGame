using GamePlay.Enemy.Spawner;
using UnityEngine;

namespace GamePlay.Enemy.Move
{
    public class MoveIndestructibleEnemy : MonoBehaviour
    {
        [SerializeField] private float _z;
        [SerializeField] private float _x;
        private const float HelpY = 90;
        private const float Y = 0;
        private float _speed=0.01f;
        private float _time;

        private void FixedUpdate()
        {
            transform.localScale= new Vector3(30,30,30);
            MoveSystem();
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                DirectioAndSpeedMovement();
            }
        }
        private void MoveSystem()
        {
            if (InfinityEnemySpawner.SpawnNumber >= 40)
            {
                _time += Time.deltaTime;
                if (_time > 5)
                {
                    _time = 0;
                    _speed += 0.0005f;
                }
                    
                transform.Translate(new Vector3(_x * _speed, 0, _z * _speed));
            }
            else
            {
                transform.Translate(new Vector3(_x * _speed, 0, _z * _speed));
            }
        }
        private void DirectioAndSpeedMovement()
        {
            transform.Rotate(0, Y + HelpY, 0);
        }
    }
}