using UnityEngine;

namespace GamePlay.Camera
{
    public class CameraController : MonoBehaviour
    {
        protected GameObject Player;
        private const float OffSetY = 15;
        private Vector3 _cameraPosition;

        private void FixedUpdate()
        {
            Camera(0.5f);
        }

        protected void Camera(float cameraSpeed)
        {
            var position = Player.transform.position;
            _cameraPosition.x = position.x;
            _cameraPosition.y = position.y + OffSetY;
            _cameraPosition.z = position.z;

            transform.position = Vector3.Lerp(transform.position, _cameraPosition, cameraSpeed);
        }
    }
}