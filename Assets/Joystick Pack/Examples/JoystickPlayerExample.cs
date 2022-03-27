using Menu.SelectionClass;
using UnityEngine;

namespace Joystick_Pack.Examples
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class JoystickPlayerExample : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _moveParticle;
        [SerializeField] private AudioSource _moveSound;
        [SerializeField] private ParticleSystem _frictionParticle;
        private VariableJoystick _variableJoystick;
        private Rigidbody _rigidbody;
        public static float Speed;
        public float MaxSpeed { get; private set; } = 20;
        private Quaternion _rotation;
        private const float RotationSpeed = 10;

        private void Start()
        {
            if (SelectionClassView.WhatPlaying == "Level")
            {
                MaxSpeed = 10;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                MaxSpeed = 20;
            }

            _moveParticle.Stop();
            _moveSound.Play();
            _moveSound.mute = true;
            Speed = MaxSpeed;
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
                        Instantiate(_frictionParticle, new Vector3(hitPoint.x, hitPoint.y, hitPoint.z),
                            transform.rotation);

                        _moveSound.mute = false;
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
                        Instantiate(_frictionParticle, new Vector3(hitPoint.x, hitPoint.y, hitPoint.z),
                            transform.rotation);
                    }

                    _moveSound.mute = false;
                }
                else
                {
                    _moveSound.mute = true;
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                _moveSound.mute = true;
            }
        }

        private void MoveCharacter()
        {
            _rigidbody.velocity = new Vector3(_variableJoystick.Horizontal * Speed, _rigidbody.velocity.y,
                _variableJoystick.Vertical * Speed);
            if (_variableJoystick.Horizontal != 0 || _variableJoystick.Vertical != 0)
            {
                _rotation = Quaternion.LookRotation(_rigidbody.velocity);
                transform.rotation = Quaternion.Lerp(transform.rotation, _rotation, RotationSpeed * Time.deltaTime);
            }

            if (_variableJoystick.Horizontal > 0.0f || _variableJoystick.Vertical > 0.0f)
            {
                _moveParticle.Play();
            }
        }
    }
}