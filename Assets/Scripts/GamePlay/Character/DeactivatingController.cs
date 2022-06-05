using UnityEngine;

namespace GamePlay.Character
{
    public class DeactivatingController : MonoBehaviour
    {
        private Material[] _materials;
        private float _timer;

        private void Start()
        {
            _materials = gameObject.transform.GetChild(0).GetComponent<Renderer>().materials;
        }

        private void Update()
        {
            if (gameObject.layer == 12)
            {
                _timer += Time.deltaTime;
                if (_timer > 6f)
                {
                    _timer = 0;
                    gameObject.layer = 8;
                    foreach (var material in _materials)
                    {
                        Color startColor = material.color;
                        material.color = new Color(startColor.r, startColor.g, startColor.b, 1f);
                    }
                }
            }
        }
    }
}