using Joystick_Pack.Examples;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character.Spell
{
    public class RespawnSpell : MonoBehaviour
    {
        [SerializeField] private JoystickPlayerExample _joystickPlayerExample;
        [SerializeField] private HealthController _healthController;
        [SerializeField] private GameObject _respawnParticle;
        [SerializeField] private Button _spell1;
        [SerializeField] private int _spellNumber = 1;
        private GameObject _killer;
        private Vector3 _diePosition;
        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
                other.gameObject.layer == LayerMask.NameToLayer("IndestructibleEnemy"))
            {
                if (_spellNumber >= 1)
                {
                    _spell1.interactable = true;
                    _killer = other.gameObject;
                    _diePosition = transform.position;
                }
            }
        }

        public void Respawn()
        {
            if (_spellNumber >= 1)
            {
                _killer.SetActive(false);
                transform.position = _diePosition;
                JoystickPlayerExample.Speed = _joystickPlayerExample.MaxSpeed;
                _healthController.HpNumber++;
                Time.timeScale = 1;
                _spellNumber--;
                _respawnParticle.SetActive(true);
                HealthController.ImmortalityTime = 0;
                HealthController.Immortality = true;
                _spell1.interactable = false;
            }
        }
    }
}