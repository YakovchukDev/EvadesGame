using System;
using System.Collections;
using DG.Tweening;
using GamePlay.Character;
using Map;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GamePlay.Map.Expirience
{
    public class ExpirienceController : MonoBehaviour
    {
        [SerializeField] private Image _fillingImage;
        [SerializeField] private TMP_Text _quantityPointsTMP;
        [SerializeField] private float _expirienceCount;
        private int _quantityPoints;
        private float _value;
        private float _targetProgress = 1;
        public static Action<bool> UpdateUpgradeMenu;
        public static event Action<int> OnGetLevelPoints;

        void Start()
        {
            _value = 0;
            _quantityPoints = 0;
            _quantityPointsTMP.text = $"{_quantityPoints}";
            _fillingImage.DOFillAmount(_value / _targetProgress, 1);
        }

        void OnEnable()
        {
            ExpirienceControl.GiveExpirience += UpdateFillAmount;
            CharacterUpdate.UseLevelPoints += UseLevelPoints;
        }

        void OnDisable()
        {
            ExpirienceControl.GiveExpirience -= UpdateFillAmount;
            CharacterUpdate.UseLevelPoints -= UseLevelPoints;
        }

        private void UseLevelPoints(int updateCost)
        {
            _quantityPointsTMP.text = $"{_quantityPoints -= updateCost}";
            OnGetLevelPoints?.Invoke(_quantityPoints);
        }

        private void UpdateFillAmount()
        {
            _value += _expirienceCount;

            while (_value >= _targetProgress)
            {
                ResetFillAmount();
                OnGetLevelPoints?.Invoke(_quantityPoints);
            }

            UpdateUpgradeMenu?.Invoke(true);
        }

        private void ResetFillAmount()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_fillingImage.DOFillAmount(_value / _targetProgress, 0.5f).OnComplete(SetNumberPoint));
            sequence.AppendInterval(0.5f);
            _quantityPoints++;
            _value--;
            sequence.Append(_fillingImage.DOFillAmount(0, 1));
            sequence.AppendInterval(0.5f);
        }

        private void SetNumberPoint()
        {
            _quantityPointsTMP.text = $"{_quantityPoints}";
        }
    }
}