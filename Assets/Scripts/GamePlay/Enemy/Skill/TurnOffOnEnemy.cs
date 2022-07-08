using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Enemy.Skill
{
    public class TurnOffOnEnemy : MonoBehaviour
    {
        [SerializeField] private Renderer _renderer;
        [SerializeField] private List<GameObject> _particles;
        private float _timeForMove;

        private void Start()
        {
            foreach (var particle in _particles)
            {
                particle.SetActive(false);
            }
        }

        private void FixedUpdate()
        {
            TurnOff_On();
        }

        private void TurnOff_On()
        {
            _timeForMove += Time.deltaTime;
            if (_timeForMove >= 5 && _timeForMove < 10)
            {
                Color bodyColor = new Color(0f, 0f, 0f, 0.3f);
                Color eyeColor = new Color(1f, 0f, 0f, 0.3f);
                Color helmetColor = new Color(0.35f, 0.35f, 0.35f, 0.3f);
                foreach (var particle in _particles)
                {
                    particle.SetActive(true);
                }

                var materials = _renderer.materials;
                materials[0].color = bodyColor;
                materials[1].color = eyeColor;
                materials[2].color = helmetColor;

                gameObject.layer = 12;
            }
            else if (_timeForMove >= 10)
            {
                Color bodyColor = new Color(0f, 0f, 0f, 1f);
                Color eyeColor = new Color(1f, 0f, 0f, 1f);
                Color helmetColor = new Color(0.35f, 0.35f, 0.35f, 1f);
                foreach (var particle in _particles)
                {
                    particle.SetActive(false);
                }

                var materials = _renderer.materials;
                materials[0].color = bodyColor;
                materials[1].color = eyeColor;
                materials[2].color = helmetColor;
                _timeForMove = 0;
                gameObject.layer = 8;
            }
        }
    }
}