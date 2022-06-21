using UnityEngine;

namespace GamePlay.Enemy.Skill
{
    public class RepulsionEnemy : MonoBehaviour
    {
        private Rigidbody _componentRigidbody;

        private void Start()
        {
            _componentRigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && other.attachedRigidbody != null)
            {
                Repulsion(other.attachedRigidbody);
            }
        }

        private void Repulsion(Rigidbody body)
        {
            var directionToSphere = (transform.position - body.position).normalized;
            var strength = body.mass * _componentRigidbody.mass * 8;

            body.AddForce(-directionToSphere * strength);
        }
    }
}