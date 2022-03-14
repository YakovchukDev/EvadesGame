using Menu.SelectionClass;
using UnityEngine;

namespace TextMesh_Pro.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private GameObject _player;
        private float _offSetY = 15;
        private Vector3 _cameraPosition;

        private void Update()
        {
            if (SelectionClassView._characterType == "Just")
            {
                _player = GameObject.Find("CharacterJust(Clone)");
            }
            else if (SelectionClassView._characterType == "Necro")
            {
                _player = GameObject.Find("CharacterNecro(Clone)");
            }
            else if (SelectionClassView._characterType == "Morfe")
            {
                _player = GameObject.Find("CharacterMorfe(Clone)");
            }
            else if (SelectionClassView._characterType == "Neo")
            {
                _player = GameObject.Find("CharacterNeo(Clone)");
            }
            else if (SelectionClassView._characterType == "Invulnerable")
            {
                _player = GameObject.Find("CharacterMagmax(Clone)");
            }
            else if (SelectionClassView._characterType == "Nexus")
            {
                _player = GameObject.Find("CharacterNexusComponent");
            }
        }

        private void LateUpdate()
        {
            var position = _player.transform.position;
            _cameraPosition.x = position.x;
            _cameraPosition.y = position.y + _offSetY;
            _cameraPosition.z = position.z;

            transform.position = _cameraPosition;
        }
    }
}