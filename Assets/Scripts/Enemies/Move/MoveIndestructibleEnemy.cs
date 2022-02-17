using UnityEngine;

namespace Enemy.Move
{
    public class MoveIndestructibleEnemy : MonoBehaviour
    {
        [SerializeField] private float z, x;
        private float helpY = 90;
        private float y = 0;
        private void FixedUpdate()
        {
            MoveSystem(0.02f);
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Wall")
            {
                DirectioAndSpeedMovement();
            }

        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Wall")
            {
                DirectioAndSpeedMovement();
            }
        }
        private void MoveSystem(float speed)
        {
            transform.Translate(new Vector3(x * speed, 0, z * speed));
        }
        private void DirectioAndSpeedMovement()
        {
            transform.Rotate(0, y + helpY, 0);
        }
    }
}