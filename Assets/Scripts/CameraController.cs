using GamePlay.Character;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private CharacterSpawner _characterSpawner;
    private GameObject _player;
    private const float OffSetY = 15;
    private Vector3 _cameraPosition;


    private void Start()
    {
        _player = _characterSpawner.Character;
        _cameraPosition.y = _player.transform.position.y + OffSetY;
    }

    private void Update()
    {
        Camera();
    }

    private void Camera()
    {
        var position = _player.transform.position;
        _cameraPosition.x = position.x;
        _cameraPosition.z = position.z;

        transform.position = _cameraPosition;
    }
}