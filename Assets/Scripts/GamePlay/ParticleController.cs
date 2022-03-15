using UnityEngine;

namespace GamePlay
{
    public class ParticleController : MonoBehaviour
    {
        [SerializeField] private ParticleSystem _particle;
        [SerializeField] private float _LifeTime;
        [SerializeField] private float _Speed;
        [SerializeField] private float _Size;

        private void Start()
        {
            if (transform.localScale.x < 30)
            {
                _particle.startLifetime = _particle.startLifetime / _LifeTime;
                _particle.startSpeed = _particle.startSpeed / _Speed;
                _particle.startSize = _particle.startSize / _Size;
            
            }
            else if (transform.localScale.x > 60)
            {
                _particle.startLifetime = _particle.startLifetime * _LifeTime;
                _particle.startSpeed = _particle.startSpeed * _Speed;
                _particle.startSize = _particle.startSize * _Size;
            }
        }
    }
}