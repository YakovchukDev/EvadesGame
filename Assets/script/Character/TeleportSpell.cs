using UnityEngine;
using UnityEngine.SceneManagement;

namespace Character
{
    public class TeleportSpell : MonoBehaviour
    {
        [SerializeField] private ManaController _manaController;
        [SerializeField] private VariableJoystick variableJoystick;
        public void TeleportSkill(float TeleportationLength)
        {
            if (_manaController.ManaReduction(10f))
            {
                Vector3 direction = Vector3.forward * variableJoystick.Vertical + Vector3.right * variableJoystick.Horizontal;
                if (SceneManager.GetActiveScene().name == "InfinityGame")
                {
                    if (direction != new Vector3(0, 0, 0))
                        transform.position = transform.position + direction * TeleportationLength;
                    else
                        transform.position = transform.position + new Vector3(0, 0, 1) * TeleportationLength;
                }
                else
                {
                    if (direction != new Vector3(0, 0, 0))
                        transform.position = transform.position + direction * TeleportationLength;
                    else
                        transform.position = transform.position + new Vector3(1, 0, 0) * TeleportationLength;
                }
            }
        }
    }
}
