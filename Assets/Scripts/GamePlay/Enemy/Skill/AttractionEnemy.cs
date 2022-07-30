using Education.Level;
using Map.Data;
using UnityEngine;

namespace GamePlay.Enemy.Skill
{
    public class AttractionEnemy : MonoBehaviour
    {
        private Rigidbody _componentRigidbody;
        private bool _inSaveZone;

        private void OnEnable()
        {
            SafeZoneParameters.OnEnter += OrSaveZone;
            EducationSaveZone.OnEnter += OrSaveZone;
        }

        private void OnDisable()
        {
            SafeZoneParameters.OnEnter -= OrSaveZone;
            EducationSaveZone.OnEnter -= OrSaveZone;
        }

        private void OrSaveZone(bool orSaveZone)
        {
            _inSaveZone = orSaveZone;
        }

        private void Start()
        {
            _componentRigidbody = GetComponent<Rigidbody>();
        }

        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Player") && other.attachedRigidbody != null && !_inSaveZone)
            {
                Attraction(other.attachedRigidbody);
            }
        }

        private void Attraction(Rigidbody body)
        {
            var directionToSphere = (transform.position - body.position).normalized;
            var strength = body.mass * _componentRigidbody.mass * 8;

            body.AddForce(directionToSphere * strength);
        }
    }
}