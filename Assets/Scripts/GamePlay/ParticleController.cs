using System.Collections.Generic;
using UnityEngine;

namespace GamePlay
{
    public class ParticleController : MonoBehaviour
    {
        [SerializeField] private List<ParticleSystem> _particles;
        [SerializeField] private float _LifeTime;
        [SerializeField] private float _Speed;
        [SerializeField] private float _Size;

        private void Start()
        {
            if (transform.localScale.x < 30)
            {
                foreach (var particle in _particles)
                {
                    particle.startLifetime = particle.startLifetime / _LifeTime;
                    particle.startSpeed = particle.startSpeed / _Speed;
                    particle.startSize = particle.startSize / _Size;
                }
            }
            else if (transform.localScale.x > 60)
            {
                foreach (var particle in _particles)
                {
                    particle.startLifetime = particle.startLifetime * _LifeTime;
                    particle.startSpeed = particle.startSpeed * _Speed;
                    particle.startSize = particle.startSize * _Size;
                }
            }

        }
    }
}