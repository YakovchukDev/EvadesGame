using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Map
{
    public class ExpirienceController : MonoBehaviour
    {
        [SerializeField] private TMP_Text _quantityTokensTMP;
        [SerializeField] private int _quantityTokens = 0;
        [SerializeField] private  float _targetProgress;
        [SerializeField] private float _value = 0;
        [SerializeField] private float _fillSpeed;
        private Slider _slider;
    
        void Awake()
        {
            _slider = gameObject.GetComponent<Slider>();    
        }
        void Start()
        {
            _quantityTokensTMP.text = $"{_quantityTokens}";
            _slider.maxValue = _targetProgress;
        }
        void OnEnable()
        {
            ExpirienceControl.GiveExpiriencs += updateSlider;
        }
        void OnDisable()
        {
            ExpirienceControl.GiveExpiriencs -= updateSlider;
        }

        private void updateSlider()
        {
            if(_slider.value < _targetProgress)
            {
                _value += _fillSpeed;
            }
        
            if(_value >= _targetProgress)
            {
                _value = 0;
                _quantityTokens++;
                _quantityTokensTMP.text = $"{_quantityTokens}";
            }
            _slider.value = _value;
        }

        public void SetTargerProgress(float newTargetProgress)
        {
            _targetProgress = _slider.value + newTargetProgress;
        }
        public void SetSpeedProgress(float newFillingSpeed)
        {
            _fillSpeed = newFillingSpeed;
        }
    }
}
