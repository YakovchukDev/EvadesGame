using Joystick_Pack.Examples;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character.Spell.SpeederInvulnerable
{
    public class AccelerateSpell : MonoBehaviour
    {
        [SerializeField] private JoystickPlayerExample _joystickPlayerExample;
        [SerializeField] private ManaController _manaController;
        [SerializeField] private float _speedAccelerate;
        [SerializeField] private GameObject _spellImage;

        private float _time;
        private float _starterTime;

        private bool _checkAccelerate;

        private void Start()
        {
            _time = 0.5f;
            _starterTime = _time;
        }

        private void Update()
        {
            if (_checkAccelerate)
            {
                _time -= Time.deltaTime;
                if (_time <= 0)
                {
                    if (!_manaController.ManaReduction(1f))
                    {
                        _checkAccelerate = false;
                    }

                    _time = _starterTime;
                }
            }
        }

        public void Accelerate()
        {
            _checkAccelerate = !_checkAccelerate;

            if (_checkAccelerate)
            {
                JoystickPlayerExample.Speed *= _speedAccelerate;
                _spellImage.SetActive(false);
            }
            else
            {
                JoystickPlayerExample.Speed = _joystickPlayerExample.MaxSpeed;
                _spellImage.SetActive(true);

            }
        }
    }
}