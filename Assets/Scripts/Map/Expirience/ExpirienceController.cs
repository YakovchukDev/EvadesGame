using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Map.Data;

namespace Map.Expirience
{
    public class ExpirienceController : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private TMP_Text _quantityTokensTMP;
        [SerializeField] private int _quantityTokens;
        [SerializeField] private  float _targetProgress;
        [SerializeField] private float _value;
        [SerializeField] private float _fillSpeed;

        void Start()
        {
            _quantityTokensTMP.text = $"{_quantityTokens}";
            _slider.maxValue = MapManager.LevelParameters.TargetExpirience;
        }
        void OnEnable()
        {
            ExpirienceControl.GiveExpiriencs += UpdateSlider;
        }
        void OnDisable()
        {
            ExpirienceControl.GiveExpiriencs -= UpdateSlider;
        }

        private void UpdateSlider()
        {
            if(_slider.value < MapManager.LevelParameters.TargetExpirience)
            {
                _value += _fillSpeed;
            }
        
            if(_value >= MapManager.LevelParameters.TargetExpirience)
            {
                _value = 0;
                _quantityTokens++;
                _quantityTokensTMP.text = $"{_quantityTokens}";
            }
            _slider.value = _value;
        }
        public void SetTargerProgress(int newTargetProgress)
        {
            MapManager.LevelParameters.TargetExpirience = newTargetProgress;
        }
        public void SetSpeedProgress(float newFillingSpeed)
        {
            _fillSpeed = newFillingSpeed;
        }
    }
}
