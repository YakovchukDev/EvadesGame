
using UnityEngine;

namespace GamePlay.Enemy.Move
{
    public class MoveIndestructibleEnemy : MonoBehaviour
    {
        private const float Z = -10;
        private const float X = 0;
        private const float Y = 0;
        private const float HelpY = 90;
        private float _speed = 0.01f;
        private float _time;

        private void FixedUpdate()
        {
            MoveSystem();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                DirectionAndSpeedMovement();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                DirectionAndSpeedMovement();
            }
        }

        private void MoveSystem()
        {
                transform.parent.Translate(new Vector3(X * _speed, 0, Z * _speed));
        }

        private void DirectionAndSpeedMovement()
        {
            transform.parent.Rotate(0, Y + HelpY, 0);
        }
    }
}