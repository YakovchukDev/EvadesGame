using GamePlay.Character;
using UnityEngine;

namespace GamePlay.Camera
{
    public class EducationCamera : CameraController
    {
        [SerializeField] private EducationSpawnCharacter _educationSpawnCharacter;
        private void Start()
        {
            Player = _educationSpawnCharacter.Character;
            Camera(100);
        }
    }
}