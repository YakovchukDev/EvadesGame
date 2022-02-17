using System.Collections.Generic;
using UnityEngine;

namespace Enemy.Skill
{
    public class RepulsionEnemy : MonoBehaviour
    {
        private HashSet<Rigidbody> affectedBodies = new HashSet<Rigidbody>();
        private Rigidbody componentRigidbody;
        void Start()
        {
            componentRigidbody = GetComponent<Rigidbody>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player" && other.attachedRigidbody != null)
            {
                affectedBodies.Add(other.attachedRigidbody);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.tag == "Player" && other.attachedRigidbody != null)
            {
                affectedBodies.Remove(other.attachedRigidbody);
            }
        }
        private void Update()
        {
            Attraction();
        }
        private void Attraction()
        {
            foreach (Rigidbody body in affectedBodies)
            {
                Vector3 directionToSphere = (transform.position - body.position).normalized;
                float strength = body.mass * componentRigidbody.mass;

                body.AddForce(-directionToSphere * strength);
            }
        }
    }
}