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
        private int _spellNumber = 1;

        private void Update()
        {
            if (_spellNumber <= 0)
            {
                _spell1.interactable = false;
            }
        }

        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
                other.gameObject.layer == LayerMask.NameToLayer("IndestructibleEnemy"))
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
                JoystickPlayerExample.Speed = _joystickPlayerExample.MaxSpeed;
                _healthController.HpNumber++;
                Time.timeScale = 1;
                _spellNumber--;
                HealthController.ImmortalityTime = 0;
                HealthController.Immortality = true;
            }
        }
    }
}