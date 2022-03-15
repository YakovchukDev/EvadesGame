using UnityEngine;
namespace Enemy.Skill
{
    public class TurnOff_OnEnemy : MonoBehaviour
    {
        private float _timeForMove = 0;
        void FixedUpdate()
        {
            TurnOff_On();
        }
        private void TurnOff_On()
        {
            _timeForMove += Time.deltaTime;
            if (_timeForMove >= 3.5 && _timeForMove < 7)
            {
                Color color = new Color(255, 255, 255, 0.5f);
                gameObject.GetComponent<Renderer>().material.color = color;
            }
            else if (_timeForMove >= 7)
            {
                Color color = new Color(255, 255, 255, 1f);
                gameObject.GetComponent<Renderer>().material.color = color;
                _timeForMove = 0;
            }
        }
    }
}