using UnityEngine;

namespace Map
{
    public class CameraController : MonoBehaviour
    {
        private GameObject _player;
        private const float OffSetY = 15;
        private Vector3 _cameraPosition;

        private void FixedUpdate()
        {
            if(_player != null)
            {
                Camera();
            }
        }
        public void SetPlayer(GameObject player)
        {
            _player = player;
        }
        private void Camera()
        {
            var position = _player.transform.position;
            _cameraPosition.x = position.x;
            _cameraPosition.y = position.y + OffSetY;
            _cameraPosition.z = position.z;

            transform.position =Vector3.Lerp(transform.position,_cameraPosition,0.5f); 
        }
    }
}