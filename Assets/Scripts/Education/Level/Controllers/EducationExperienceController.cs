using System;
using System.Collections;
using DG.Tweening;
using Education.Level.Controls;
using GamePlay.Character;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Education.Level.Controllers
{
    public class EducationExperienceController : MonoBehaviour
    {
        public int QuantityPoints { get; private set; }
        [SerializeField] private GameObject _backGround;
        [SerializeField] private Image _fillingImage;
        [SerializeField] private TMP_Text _quantityPointsTMP;
        [SerializeField] private float _expirienceCount;
        private float _value;
        private float _targetProgress = 1;
        public static Action<bool> UpdateUpgradeMenu;
        public static event Action<int> OnGetLevelPoints;

        void Start()
        {
            QuantityPoints = 0;
            _quantityPointsTMP.text = $"{QuantityPoints}";
        }

        void OnEnable()
        {
            EducationExperienceControl.GiveExperience += UpdateFillAmount;
            CharacterUpdate.UseLevelPoints += UseLevelPoints;
        }

        void OnDisable()
        {
            EducationExperienceControl.GiveExperience -= UpdateFillAmount;
            CharacterUpdate.UseLevelPoints -= UseLevelPoints;
        }

        private void UseLevelPoints(int updateCost)
        {
            _quantityPointsTMP.text = $"{QuantityPoints -= updateCost}";
            OnGetLevelPoints?.Invoke(QuantityPoints);
            if (QuantityPoints <= 0)
            {
                _backGround.SetActive(false);
            }
        }
        
        private void UpdateFillAmount()
        {
            _value += _expirienceCount;

            while (_value >= _targetProgress)
            {
                ResetFillAmount();
                OnGetLevelPoints?.Invoke(QuantityPoints);
            }

            UpdateUpgradeMenu?.Invoke(true);
        }

        private void ResetFillAmount()
        {
            var sequence = DOTween.Sequence();
            sequence.Append(_fillingImage.DOFillAmount(_value / _targetProgress, 0.5f).OnComplete(SetNumberPoint));
            sequence.AppendInterval(0.5f);
            QuantityPoints++;
            _value--;
            sequence.Append(_fillingImage.DOFillAmount(0, 1));
            sequence.AppendInterval(0.5f);
        }

        private void SetNumberPoint()
        {
            _quantityPointsTMP.text = $"{QuantityPoints}";
        }
    }
}