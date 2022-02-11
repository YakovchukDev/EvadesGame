using UnityEngine;

namespace Enemy.Move
{
    public class MoveVerticalEnemy : MoveEnemy
    {
        [SerializeField] private float z;
        private float helpY = 180, y = 0;
        private void Start()
        {
            DirectioAndSpeedMovement(10.0f);
        }
        private void FixedUpdate()
        {
            MoveSystem(0.02f);
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.tag == "Wall")
            {
                Invoke("Direction", 1);
            }
        }
        private void Direction()
        {
            transform.Rotate(0, y + helpY, 0);
        }
        private void MoveSystem(float speed)
        {
            transform.Translate(new Vector3(0, 0, z * speed));
        }
        public new void DirectioAndSpeedMovement(float speedAndDirection)
        {
            z = Random.Range(1, 3);
            if (z == 1)
            {
                z = -10;
            }
            else
                z = 10;
        }
    }
}