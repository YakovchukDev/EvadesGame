using UnityEngine;
using System.Collections;

namespace Enemy.Move
{
    public class MoveEnemy : MonoBehaviour
    {
        [SerializeField] private GameObject _freezField;

        private float x;
        private float z;
        private float helpZ;

        private float _freezeTimer = 0;
        private bool _canFreez = false;

        private void Start()
        {
            DirectioAndSpeedMovement(10.0f);
        }
        private void FixedUpdate()
        {
            _freezField = GameObject.FindGameObjectWithTag("Freez");

            if (_canFreez == false)
            {
                MoveSystem(0.02f);
            }
            else
            {
                FreezMe2(3);
            }
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
            if (other.tag == "Freez")
            {
                _freezeTimer = 0;
                _canFreez = true;
            }
        }

        public void AcceptZ()
        {
            if (z < 0)
                z -= z * 2;
            else
                z = -z;
        }
        public void AcceptX()
        {
            if (x < 0)
                x -= x * 2;
            else
                x = -x;
        }
        private void MoveSystem(float speed)
        {
            transform.Translate(new Vector3(x * speed, 0, z * speed));
        }
        public void DirectioAndSpeedMovement(float speedAndDirection)
        {
            float checker = speedAndDirection;

            x = Random.Range(-speedAndDirection, speedAndDirection);
            if (x > 0)
            {
                helpZ = checker - x;
                z = Random.Range(1, 3);

                if (z == 1)
                    z = -helpZ;
                else if (z == 2)
                    z = helpZ;
            }
            else if (x < 0)
            {
                helpZ = checker + x;
                z = Random.Range(1, 3);

                if (z == 1)
                    z = -helpZ;
                else if (z == 2)
                    z = helpZ;
            }
            else
            {
                z = Random.Range(1, 3);

                if (z == 1)
                    z = -10;
                else if (z == 2)
                    z = 10;
            }
        }
        private void FreezMe2(float allFreezeTime)
        {
            MoveSystem(0);
            _freezeTimer += Time.deltaTime;
            if (_freezeTimer >= allFreezeTime)
            {
                _canFreez = false;
            }
        }

    }
}