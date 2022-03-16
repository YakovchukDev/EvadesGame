using System;
using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Enemy.Skill
{
    public class TurnOffOnEnemy : MonoBehaviour
    {
        private float _timeForMove;
        [SerializeField] private List<GameObject> _particles;

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
                Color color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
                foreach (var particle in _particles)
                {
                    particle.SetActive(true);
                }

                for (int i = 0; i < gameObject.transform.GetChild(0).GetComponent<Renderer>().materials.Length; i++)
                {
                    gameObject.transform.GetChild(0).GetComponent<Renderer>().materials[i].color = color;
                }


                gameObject.layer = 12;
            }
            else if (_timeForMove >= 10)
            {
                Color color = new Color(0.3f, 0.3f, 0.3f, 1f);
                foreach (var particle in _particles)
                {
                    particle.SetActive(false);
                }

                for (int i = 0; i < gameObject.transform.GetChild(0).GetComponent<Renderer>().materials.Length; i++)
                {
                    gameObject.transform.GetChild(0).GetComponent<Renderer>().materials[i].color = color;
                }

                _timeForMove = 0;
                gameObject.layer = 8;
            }
        }
    }
}