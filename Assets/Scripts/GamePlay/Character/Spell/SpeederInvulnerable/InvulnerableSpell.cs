 using Joystick_Pack.Examples;
using UnityEngine;

namespace GamePlay.Character.Spell.SpeederInvulnerable
{
    public class InvulnerableSpell : MonoBehaviour
    {
        [SerializeField] private JoystickPlayerExample _joystickPlayerExample;
        [SerializeField] private ManaController _manaController;
        [SerializeField] private float _maxManaCost;
        private float _time;
        private float _starterTime;
        [SerializeField] private GameObject _spellImage;


        private bool _checkMover;

        private void Start()
        {
            _time = 0.1f;
            _starterTime = _time;
        }

        private void Update()
        {
            if (_checkMover)
            {
                _time -= Time.deltaTime;
                if (_time <= 0)
                {
                    if (!_manaController.ManaReduction(_maxManaCost))
                    {
                        _checkMover = false;
                    }

                    _time = _starterTime;
                }
            }
        }

        public void Invulnerable()
        {
            _checkMover = !_checkMover;

            if (_checkMover)
            {
                JoystickPlayerExample.Speed = 0;
                gameObject.layer = 11;
                _spellImage.SetActive(false);

            }
            else
            {
                JoystickPlayerExample.Speed = _joystickPlayerExample.MaxSpeed;
                gameObject.layer = 6;
                _spellImage.SetActive(true);

            }
        }
    }
}