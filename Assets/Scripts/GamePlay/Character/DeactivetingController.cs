using UnityEngine;

namespace GamePlay.Character
{
    public class DeactivetingController : MonoBehaviour
    {
        private float _timer;
        void Update()
        {
            if (gameObject.layer == 12)
            {
                _timer += Time.deltaTime;
                if (_timer>6f)
                {
                    _timer = 0;
                    gameObject.layer = 8;
                    Color startColor = GetComponent<Renderer>().material.color;
                    GetComponent<Renderer>().material.color = new Color(startColor.r, startColor.g, startColor.b, 1);
                }
            }
        }
    }
}
