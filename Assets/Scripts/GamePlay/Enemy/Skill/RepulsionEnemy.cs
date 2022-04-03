using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Enemy.Skill
{
    public class RepulsionEnemy : MonoBehaviour
    {
        private readonly HashSet<Rigidbody> _affectedBodies = new HashSet<Rigidbody>();
        private Rigidbody _componentRigidbody;
        void Start()
        {
            _componentRigidbody = GetComponent<Rigidbody>();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player") && other.attachedRigidbody != null)
            {
                _affectedBodies.Add(other.attachedRigidbody);
            }
        }
        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Player") && other.attachedRigidbody != null)
            {
                _affectedBodies.Remove(other.attachedRigidbody);
            }
        }
        private void FixedUpdate()
        {
            if (Time.timeScale > 0)
            {
                Attraction();
            }
        }
        private void Attraction()
        {
            foreach (var body in _affectedBodies)
            {
                var directionToSphere = (transform.position - body.position).normalized;
                var strength = body.mass * _componentRigidbody.mass*8;

                body.AddForce(-directionToSphere * strength);
            }
        }
    }
}