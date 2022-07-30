using Audio;
using Menu.SelectionClass;
using UnityEngine;

namespace Joystick_Pack.Examples
{
    [RequireComponent(typeof(Rigidbody), typeof(SphereCollider))]
    public class JoystickPlayerExample : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _moveParticle;
        [SerializeField] private ParticleSystem _frictionParticle;
        private AudioManager _audioManager;
        private VariableJoystick _variableJoystick;
        private Rigidbody _rigidbody;
        public static float Speed;
        public float MaxSpeed { get; private set; }
        private Quaternion _rotation;
        private const float RotationSpeed = 10;

        private void Start()
        {
            _audioManager = AudioManager.Instanse;
            if (SelectionClassView.WhatPlaying == "Level")
            {
                MaxSpeed = 10;
            }
            else if (SelectionClassView.WhatPlaying == "Infinity")
            {
                MaxSpeed = 20;
            }
            else
            {
                MaxSpeed = 10;
            }

            _moveParticle.Stop();
            _audioManager.IsMute("Friction", true);
            _audioManager.Play("Friction");
            Speed = MaxSpeed;
            _rigidbody = GetComponent<Rigidbody>();
        }

        public void FixedUpdate()
        {
            _variableJoystick = FindObjectOfType<VariableJoystick>();
            MoveCharacter();
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                if (_variableJoystick.Horizontal != 0 || _variableJoystick.Vertical != 0)
                {
                    Friction(other);
                }
            }
        }

        private void OnCollisionStay(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                if (_variableJoystick.Horizontal != 0 || _variableJoystick.Vertical != 0)
                {
                    Friction(other);
                }
                else
                {
                    _audioManager.IsMute("Friction", true);
                }
            }
        }

        private void OnCollisionExit(Collision other)
        {
            if (other.gameObject.CompareTag("Wall"))
            {
                _audioManager.IsMute("Friction", true);
            }
        }

        private void Friction(Collision other)
        {
            foreach (ContactPoint missileHit in other.contacts)
            {
                Vector3 hitPoint = missileHit.point;
                _frictionParticle.transform.position = new Vector3(hitPoint.x, transform.position.y, hitPoint.z);
                _frictionParticle.transform.rotation = transform.rotation;
                _frictionParticle.Play();
            }

            _audioManager.IsMute("Friction", false);
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