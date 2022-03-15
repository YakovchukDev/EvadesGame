using System;
using UnityEngine;

namespace GamePlay.Enemy.Skill
{
    public class TurnOffOnEnemy : MonoBehaviour
    {
        private float _timeForMove;
        void FixedUpdate()
        {
            TurnOff_On();
        }
        private void TurnOff_On()
        {
            _timeForMove += Time.deltaTime;
            if (_timeForMove >= 5 && _timeForMove < 10)
            {
                Color color = new Color(0.3f, 0.3f, 0.3f, 0.3f);
                for (int i = 0; i < gameObject.transform.GetChild(0).GetComponent<Renderer>().materials.Length; i++)
                {
                    gameObject.transform.GetChild(0).GetComponent<Renderer>().materials[i].color = color; 
                }
                

                gameObject.layer = 12;
            }
            else if (_timeForMove >= 10)
            {
                Color color = new Color(0.3f, 0.3f, 0.3f, 1f);
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