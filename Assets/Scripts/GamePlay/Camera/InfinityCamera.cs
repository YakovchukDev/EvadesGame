using GamePlay.Character;
using UnityEngine;

namespace GamePlay.Camera
{
    public class InfinityCamera : CameraController
    {
        [SerializeField] private CharacterSpawner _characterSpawner;
        private void Start()
        {
            Player = _characterSpawner.Character;
            Camera(100);
        }
    }
}