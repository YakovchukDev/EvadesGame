using Menu.SelectionClass;
using UnityEngine;

namespace TextMesh_Pro.Scripts
{
    public class CameraController : SelectionClassView
    {
        private readonly string[] _characterObject =
        {
            "CharacterJust(Clone)", "CharacterNecro(Clone)", "CharacterMorfe(Clone)", "CharacterNeo(Clone)",
            "CharacterMagmax(Clone)", "CharacterNexusComponent"
        };

        [SerializeField] private GameObject _player;
        private const float OffSetY = 15;
        private Vector3 _cameraPosition;

        private void Start()
        {
            if (_player == null)
            {
                _player = GameObject.Find(_characterObject[CharacterType]);
            }
        }

        private void LateUpdate()
        {
            var position = _player.transform.position;
            _cameraPosition.x = position.x;
            _cameraPosition.y = position.y + OffSetY;
            _cameraPosition.z = position.z;

            transform.position = _cameraPosition;
        }
    }
}