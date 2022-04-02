using Menu.SelectionClass;
using UnityEngine;

namespace TextMesh_Pro.Scripts
{
    public class CameraController : MonoBehaviour
    {
        private readonly string[] _characterObject =
        {
            "CharacterWeak(Clone)", "CharacterNecro(Clone)", "CharacterShooter(Clone)", "CharacterNeo(Clone)",
            "CharacterTank(Clone)", "CharacterNecromusComponent"
        };

        [SerializeField] private GameObject _player;
        private const float OffSetY = 15;
        private Vector3 _cameraPosition;

        private void Start()
        {
            if (_player == null)
            {
                _player = GameObject.Find(_characterObject[SelectionClassView.CharacterType]);
            }
        }

        private void LateUpdate()
        {
            if (_player == null)
            {
                _player = GameObject.Find(_characterObject[SelectionClassView.CharacterType]);
            }
            var position = _player.transform.position;
            _cameraPosition.x = position.x;
            _cameraPosition.y = position.y + OffSetY;
            _cameraPosition.z = position.z;

            transform.position = _cameraPosition;
        }
    }
}