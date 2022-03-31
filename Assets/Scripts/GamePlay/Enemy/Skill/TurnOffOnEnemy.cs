using System.Collections.Generic;
using UnityEngine;

namespace GamePlay.Enemy.Skill
{
    public class TurnOffOnEnemy : MonoBehaviour
    {
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
                Color Bodycolor = new Color(0f, 0f, 0f, 0.3f);
                Color Eyecolor = new Color(1f, 0f, 0f, 0.3f);
                Color Helmetcolor = new Color(0.35f, 0.35f, 0.35f, 0.3f);
                foreach (var particle in _particles)
                {
                    particle.SetActive(true);
                }
                gameObject.transform.GetChild(0).GetComponent<Renderer>().materials[0].color = Bodycolor;
                gameObject.transform.GetChild(0).GetComponent<Renderer>().materials[1].color = Eyecolor;
                gameObject.transform.GetChild(0).GetComponent<Renderer>().materials[2].color = Helmetcolor;
                /*(var material in gameObject.transform.GetChild(0).GetComponent<Renderer>().materials)
                {
                    material.color = color;
                }*/


                gameObject.layer = 12;
            }
            else if (_timeForMove >= 10)
            {
                Color Bodycolor = new Color(0f, 0f, 0f, 1f);
                Color Eyecolor = new Color(1f, 0f, 0f, 1f);
                Color Helmetcolor = new Color(0.35f, 0.35f, 0.35f, 1f);
                foreach (var particle in _particles)
                {
                    particle.SetActive(false);
                }
                gameObject.transform.GetChild(0).GetComponent<Renderer>().materials[0].color = Bodycolor;
                gameObject.transform.GetChild(0).GetComponent<Renderer>().materials[1].color = Eyecolor;
                gameObject.transform.GetChild(0).GetComponent<Renderer>().materials[2].color = Helmetcolor;
                /*foreach (var material in gameObject.transform.GetChild(0).GetComponent<Renderer>().materials)
                {
                    material.color = color;
                }*/

                _timeForMove = 0;
                gameObject.layer = 8;
            }
        }
    }
}