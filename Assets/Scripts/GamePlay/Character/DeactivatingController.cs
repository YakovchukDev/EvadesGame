using System;
using UnityEngine;

namespace GamePlay.Character
{
    public class DeactivatingController : MonoBehaviour
    {
        private float _timer;
        private Material[] _materials;

        private void Start()
        {
            _materials = gameObject.transform.GetChild(0).GetComponent<Renderer>().materials;
        }

        void Update()
        {
            if (gameObject.layer == 12)
            {
                _timer += Time.deltaTime;
                if (_timer > 6f)
                {
                    _timer = 0;
                    gameObject.layer = 8;
                    for (int i = 0; i < _materials.Length; i++)
                    {
                        Color startColor = _materials[i].color;
                        _materials[i].color = new Color(startColor.r, startColor.g, startColor.b, 1f);
                    }
                }
            }
        }
    }
}