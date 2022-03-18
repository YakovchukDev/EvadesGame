using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Character
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private int _hpNumber = 1;
        [SerializeField] private GameObject _panelAfterDie;
        [SerializeField] private List<Button> _spellButtons;
        private Color _materialColor;
        private Color _color1;
        private Color _color2;
        public static float ImmortalityTime;
        public static bool Immortality;

        public int HpNumber
        {
            get => _hpNumber;
            set => _hpNumber = value;
        }

        private void Start()
        {
            _materialColor = gameObject.transform.GetComponent<Renderer>().materials[0].color;
            _color1 = new Color(_materialColor.r, _materialColor.g, _materialColor.b, 0.3f);
            _color2 = new Color(_materialColor.r, _materialColor.g, _materialColor.b, 1f);
            gameObject.transform.GetComponent<Renderer>().materials[0].color = _color1;
            ImmortalityTime = 0;
            Immortality = true;
        }

        private void Update()
        {
            HpImmortality();
        }


        private void OnCollisionEnter(Collision other)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Enemy") ||
                other.gameObject.layer == LayerMask.NameToLayer("IndestructibleEnemy"))
            {
                /*if (_spellButtons != null)
                {
                    foreach (var spell in _spellButtons)
                    {
                        spell.interactable = true;
                    }
                }*/

                gameObject.transform.GetComponent<Renderer>().materials[0].color = _color1;
                transform.localScale = Vector3.one;
                _hpNumber--;
                HpChecker();
                other.gameObject.SetActive(false);
            }
        }

        private void HpImmortality()
        {
            if (Immortality)
            {
                ImmortalityTime += Time.deltaTime;
                gameObject.layer = 11;
                gameObject.transform.GetComponent<Renderer>().materials[0].color =
                    Color.Lerp(_color1, _color2, ImmortalityTime);
                if (ImmortalityTime >= 1.6f)
                {
                    gameObject.layer = 6;
                    Immortality = false;
                }
            }
        }

        private void HpChecker()
        {
            if (_hpNumber <= 0)
            {
                Time.timeScale = 0;
                _panelAfterDie.SetActive(true);
                InterfaceController.TimeSave();
            }
        }
    }
}