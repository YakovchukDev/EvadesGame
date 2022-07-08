using GamePlay.Enemy.ForInfinity.Spawner;
using UnityEngine;

namespace GamePlay.Enemy.Move
{
    public class MoveIndestructible : EnemyMove
    {
        [SerializeField] private GameObject _spikes;
        private const float LengthForTurn = 1.38f;
        private const float Z = -10;
        private const float X = 0;
        private const float Turn = 90;

        private void FixedUpdate()
        {
            transform.localScale = new Vector3(30, 30, 30);
            MoveSystem(_speed);
            ChangeDirection(Turn); // "+" - left; "-" - right;
            SpikeRotate();
        }

        protected override void MoveSystem(float speed)
        {
            if (InfinityEnemySpawner.SpawnNumber >= 40)
            {
                FasterTime += Time.deltaTime;
                if (FasterTime > 5)
                {
                    FasterTime = 0;
                    _speed += 0.0005f;
                }
            }

            transform.Translate(new Vector3(X * _speed, 0, Z * _speed));
        }

        private void ChangeDirection(float turn)
        {
            Ray ray = new Ray(transform.position, -transform.forward);
            if (Physics.Raycast(ray, out var hit))
            {
                if (Physics.Raycast(ray, out hit, LengthForTurn))
                {
                    if (hit.transform.gameObject.CompareTag("Wall"))
                    {
                        transform.Rotate(0, turn, 0);
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