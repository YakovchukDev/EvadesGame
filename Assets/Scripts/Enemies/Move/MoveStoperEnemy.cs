using UnityEngine;

namespace Enemy.Move
{
    public class MoveStoperEnemy : MoveEnemy
    {
        [SerializeField] private float x;
        [SerializeField] private float z;
        private float _timeForMove = 0;
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
                AcceptZ();
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Wall")
            {
                AcceptX();
            }
        }
        private void MoveSystem(float speed)
        {
            _timeForMove += Time.deltaTime;
            if (_timeForMove <= 3.5)
            {
                transform.Translate(new Vector3(x * speed, 0, z * speed));
            }
            else if (_timeForMove >= 7)
            {
                _timeForMove = 0;
            }
        }
    }
}