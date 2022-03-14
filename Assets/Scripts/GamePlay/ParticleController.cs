using UnityEngine;

public class ParticleController : MonoBehaviour
{
    [SerializeField] private ParticleSystem _particle;

    private void Start()
    {
        if (transform.localScale.x < 30)
        {
            _particle.startLifetime = _particle.startLifetime / 2;
            _particle.startSpeed = _particle.startSpeed / 2;
            _particle.startSize = _particle.startSize / 2;
            
        }
        else if (transform.localScale.x > 60)
        {
            _particle.startSpeed = _particle.startSpeed * 2.2f;
            _particle.startSize = _particle.startSize * 2.2f;
            _particle.startLifetime = _particle.startLifetime * 2.2f;
        }
    }
}