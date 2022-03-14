using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay.Character.Spell.TelepFreeze
{
    public class TeleportSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private ReloadSpell _reloadSpell;
        private VariableJoystick _variableJoystick;

        [SerializeField] private float _teleportationLength;
        [SerializeField] private float _manaCost;

        private void Start()
        {
            _variableJoystick = FindObjectOfType<VariableJoystick>();
        }

        public void TeleportSkill()
        {
            if (_reloadSpell._canUseSpellSecond)
            {
                _reloadSpell.ReloadSecondSpell(1);
                if (_manaController.ManaReduction(_manaCost))
                {
                    Vector3 direction = Vector3.forward * _variableJoystick.Vertical +
                                        Vector3.right * _variableJoystick.Horizontal;
                    if (SceneManager.GetActiveScene().name == "InfinityGame")
                    {
                        if (direction != new Vector3(0, 0, 0))
                            transform.position = transform.position + direction * _teleportationLength;
                        else
                            transform.position += transform.forward * _teleportationLength;
                    }
                    else
                    {
                        if (direction != new Vector3(0, 0, 0))
                            transform.position = transform.position + direction * _teleportationLength;
                        else
                            transform.position = transform.position + new Vector3(1, 0, 0) * _teleportationLength;
                    }
                }
            }
        }
    }
}