using UnityEngine;

namespace GamePlay.Enemy.Move
{
    public class MoveVerticalEnemy : MoveEnemy
    {
        [SerializeField] private float _z;
        private float helpY = 180, y = 0;
        private void Start()
        {
           DirectioAndSpeedMovement(10.0f);
        }
        private void FixedUpdate()
        {
           _freezeField = GameObject.FindGameObjectWithTag("Freeze");

            if (CanFreeze == false)
            {
                MoveSystem(0.01f);
            }
            else
            {
                FreezeMe(3);
            }
        }
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("WallX"))
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
            transform.Translate(new Vector3(0, 0, _z * speed));
        }
        public new void DirectioAndSpeedMovement(float speedAndDirection)
        {
            _z = Random.Range(1, 3);
            if (_z == 1)
            {
                _z = -10;
            }
            else
                _z = 10;
        }
    }
}