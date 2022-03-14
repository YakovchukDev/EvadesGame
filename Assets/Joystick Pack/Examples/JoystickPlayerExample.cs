using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Joystick_Pack.Examples
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class JoystickPlayerExample : MonoBehaviour
    {
        public static float Speed;
        [SerializeField] private float _maxSpeed;
        private VariableJoystick _variableJoystick;
        private Rigidbody _rigidbody;
        private Vector3 _direction;
        private float _frictionTime;
        [SerializeField] private List<GameObject> _patronMinimize;
        [SerializeField] private List<GameObject> _patronDeactivating;
        [SerializeField] private ParticleSystem _moveParticle;
        [SerializeField] private ParticleSystem _frictionParticle;
        public Vector3 Direction => _direction;

        public float MaxSpeed => _maxSpeed;

        private void Start()
        {
            _moveParticle.Stop();

            Speed = _maxSpeed;
            _rigidbody = GetComponent<Rigidbody>();
            _variableJoystick = FindObjectOfType<VariableJoystick>();
        }

        public void FixedUpdate()
        {
            MoveCharacter();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                if (_variableJoystick.Horizontal != 0 || _variableJoystick.Vertical != 0)
                {
                    foreach (ContactPoint missileHit in other.contacts)
                    {
                        Vector3 hitPoint = missileHit.point;
                        Instantiate(_frictionParticle, new Vector3(hitPoint.x, hitPoint.y, hitPoint.z), transform.rotation);
                    }
                }
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                if (_variableJoystick.Horizontal != 0 || _variableJoystick.Vertical != 0)
                {
                    foreach (ContactPoint missileHit in other.contacts)
                    {
                        Vector3 hitPoint = missileHit.point;
                        Instantiate(_frictionParticle, new Vector3(hitPoint.x, hitPoint.y, hitPoint.z), transform.rotation);
                    }
                }
            }
        }


        private void MoveCharacter()
        {
            _rigidbody.velocity = new Vector3(_variableJoystick.Horizontal * Speed, _rigidbody.velocity.y,
                _variableJoystick.Vertical * Speed);
            if (_variableJoystick.Horizontal != 0 || _variableJoystick.Vertical != 0)
            {
                transform.rotation = Quaternion.LookRotation(_rigidbody.velocity);
            }

            if (_variableJoystick.Horizontal > 0.0f || _variableJoystick.Vertical > 0.0f)
            {
                _moveParticle.Play();
            }
        }
    }
}