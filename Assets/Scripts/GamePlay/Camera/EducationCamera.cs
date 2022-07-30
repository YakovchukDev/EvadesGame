using GamePlay.Character;
using UnityEngine;

namespace GamePlay.Camera
{
    public class EducationCamera : CameraController
    {
        [SerializeField] private CharacterSpawner _characterSpawner;
        private void Start()
        {
            Player = _characterSpawner.Character;
            Camera(100);
        }
    }
}