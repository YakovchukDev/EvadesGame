using Menu.Settings;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Serialization;
using System;

namespace GamePlay.Character
{
    public class HealthController : MonoBehaviour
    {
        [SerializeField] private SelectUIPosition _selectUIPosition;
        [SerializeField] private int _hpNumber = 3;
        [SerializeField] private AudioSource _dieSound;
        [SerializeField] private AudioMixerGroup _audioMixer;
        public static float ImmortalityTime;
        public static bool Immortality;
        /*private Color _materialColor;
        private Color _color1;
        private Color _color2;
        private Renderer _renderer;*/

        [SerializeField] private Animator _leftDieAnimator;
        [SerializeField] private Animator _rightDieAnimator;
       

        public int HpNumber
        {
            get => _hpNumber;
            set => _hpNumber = value;
        }
        public static event Action OnBackToSafeZone;
        public static event HealthChange HealthPanelUpdate;       
        public delegate void HealthChange(int hpCount);

        private void Start()
        {
            /*_renderer = gameObject.transform.GetComponent<Renderer>();
            _materialColor = gameObject.transform.GetComponent<Renderer>().materials[0].color;
            _color1 = new Color(_materialColor.r, _materialColor.g, _materialColor.b, 0.3f);
            _color2 = new Color(_materialColor.r, _materialColor.g, _materialColor.b, 1f);*/
           // gameObject.transform.GetComponent<Renderer>().materials[0].color = _color1;
            ImmortalityTime = 0;
            Immortality = true;
            
            HealthPanelUpdate(_hpNumber);
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
               // gameObject.transform.GetComponent<Renderer>().materials[0].color = _color1;
                transform.localScale = Vector3.one;
                _hpNumber--;
                OnBackToSafeZone();
                HealthPanelUpdate(_hpNumber);
                Immortality = true;
                HpChecker();
            }
        }

        private void HpImmortality()
        {
            if (Immortality)
            {
                ImmortalityTime += Time.deltaTime;
                gameObject.layer = 11;
                /*_renderer.materials[0].color =
                    Color.Lerp(_color1, _color2, ImmortalityTime);*/
                if (ImmortalityTime >= 1.6f)
                {
                    gameObject.layer = 6;
                    Immortality = false;
                    ImmortalityTime = 0;
                }
            }
        }

        private void HpChecker()
        {
            if (_hpNumber <= 0)
            {
                Time.timeScale = 0;
                InfinityInterfaceController.TimeSave();
                _dieSound.Play();
                _audioMixer.audioMixer.SetFloat("EffectVolume", -80);
                _selectUIPosition.SelectDiePanelPosition();
                DieOpen();
            }
        }
        public void DieOpen()
        {
            _leftDieAnimator.SetInteger("LeftDie", 0);
            _rightDieAnimator.SetInteger("RightDie", 0);
        }
    }
}