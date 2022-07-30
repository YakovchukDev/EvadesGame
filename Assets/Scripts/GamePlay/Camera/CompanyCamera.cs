using UnityEngine;

namespace GamePlay.Camera
{
    public class CompanyCamera : CameraController
    {
        public void SetPlayer(GameObject player)
        {
            Player = player;
            Camera(100);
        }
    }
}