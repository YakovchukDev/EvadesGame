using Joystick_Pack.Examples;
using UnityEngine;

namespace GamePlay.Character.Spell.SpeederInvulnerable
{
    public class AccelerateSpell : MonoBehaviour
    {
        [SerializeField] private JoystickPlayerExample _joystickPlayerExample;
        [SerializeField] private ManaController _manaController;
        [SerializeField] private GameObject _spellImage;
        [SerializeField] private GameObject _accelerationParticle;
        [SerializeField] private float _speedAccelerate;
        private float _time;
        private float _starterTime;
        public bool CheckAccelerate { get; set; }


        private void Start()
        {
            _time = 0.5f;
            _starterTime = _time;
        }

        private void Update()
        {
            if (CheckAccelerate)
            {
                _time -= Time.deltaTime;
                if (_time <= 0)
                {
                    if (!_manaController.ManaReduction(1f))
                    {
                        CheckAccelerate = false;
                    }

                    _time = _starterTime;
                }
            }
        }

        public void Accelerate()
        {
            CheckAccelerate = !CheckAccelerate;

            if (CheckAccelerate)
            {
                JoystickPlayerExample.Speed *= _speedAccelerate;
                _spellImage.SetActive(false);
                _accelerationParticle.SetActive(true);
            }
            else
            {
                JoystickPlayerExample.Speed = _joystickPlayerExample.MaxSpeed;
                _spellImage.SetActive(true);
                _accelerationParticle.SetActive(false);
            }
        }
    }
}