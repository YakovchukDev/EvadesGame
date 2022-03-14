using Joystick_Pack.Examples;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character.Spell
{
    public class RespawnSpell : MonoBehaviour
    {
        [SerializeField] private JoystickPlayerExample _joystickPlayerExample;
        [SerializeField] private HealthController _healthController;
        [SerializeField] private Button _spell1;
        private bool _invulnerableSwitcher;
        private float _invulnerableTimer;
        private int _spellNumber = 1;

        private void Update()
        {
            if (_invulnerableSwitcher)
            {
                gameObject.layer = 11;
                _invulnerableTimer += Time.deltaTime;
                if (_invulnerableTimer >= 2)
                {
                    gameObject.layer = 0;
                    _invulnerableTimer = 0;
                    _invulnerableSwitcher = false;
                }
            }

            if (_spellNumber<=0)
            {
                _spell1.interactable = false;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy")||other.gameObject.layer == LayerMask.NameToLayer("IndestructibleEnemy"))
            {
                if (_spellNumber >= 1)
                {
                    _spell1.interactable = true;
                }
            }
        }

        public void Respawn()
        {
            if (_spellNumber >= 1)
            {
                _invulnerableSwitcher = true;
                JoystickPlayerExample.Speed = _joystickPlayerExample.MaxSpeed;
                _healthController.HpNumber++;
                Time.timeScale = 1;
                _spellNumber--;
            }
        }
    }
}