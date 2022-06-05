using GamePlay.Enemy.Spawner;
using UnityEngine;
using UnityEngine.Serialization;

namespace GamePlay.Enemy.Move
{
    public class MoveIndestructibleEnemy : MonoBehaviour
    {
        [FormerlySerializedAs("Spikes")] [SerializeField]
        private GameObject _spikes;

        private const float LengthForTurn = 1.38f;
        private const float Z = -10;
        private const float X = 0;
        private const float Y = 0;
        private const float HelpY = 90;
        private float _speed = 0.01f;
        private float _time;

        private void FixedUpdate()
        {
            transform.localScale = new Vector3(30, 30, 30);
            MoveSystem();
            DirectionAndSpeedMovement();
            SpikeRotate();
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

                transform.Translate(new Vector3(X * _speed, 0, Z * _speed));
            }
            else
            {
                transform.Translate(new Vector3(X * _speed, 0, Z * _speed));
            }
        }

        private void DirectionAndSpeedMovement()
        {
            Ray ray = new Ray(transform.position, -transform.forward);
            if (Physics.Raycast(ray, out var hit))
            {
                if (Physics.Raycast(ray, out hit, LengthForTurn))
                {
                    if (hit.transform.gameObject.CompareTag("Wall"))
                    {
                        transform.Rotate(0, Y + HelpY, 0);
                    }
                }
            }
        }

        private void SpikeRotate()
        {
            _spikes.transform.Rotate(0, 0, -3);
        }
    }
}