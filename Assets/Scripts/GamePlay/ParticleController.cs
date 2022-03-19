using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class ParticleController : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _particles;
        [SerializeField] private float _lifeTime;
        [SerializeField] private float _speed;
        [SerializeField] private float _size;

        private void Start()
        {
            if (transform.localScale.x < 30)
            {
                foreach (var particle in _particles)
                {
                    particle.startLifetime = particle.startLifetime / _lifeTime;
                    particle.startSpeed = particle.startSpeed / _speed;
                    particle.startSize = particle.startSize / _size;
                }
            }
            else if (transform.localScale.x > 60)
            {
                foreach (var particle in _particles)
                {
                    particle.startLifetime = particle.startLifetime * _lifeTime;
                    particle.startSpeed = particle.startSpeed * _speed;
                    particle.startSize = particle.startSize * _size;
                }
            }

        }
    }
}