using System;
using Menu.SelectionClass;
using TMPro;
using UnityEngine;

namespace Menu
{
    public class MenuAnimatorController : MonoBehaviour
    {
        [SerializeField] private Animator _settingsAnimator;
        [SerializeField] private Animator _informationAnimator;
        [SerializeField] private Animator _selectionAnimator;
        [SerializeField] private Animator _educationSelectionAnimator;
        [SerializeField] private Animator _companyUnlockedAnimator;
        [SerializeField] private Animator _companyLockedAnimator;
        [SerializeField] private Animator _infoAnimator;
        [SerializeField] private Animator _shopAnimator;
        [SerializeField] private Animator _gamePlaySurvival;
        [SerializeField] private Animator _gamePlayCompany;

        private void Start()
        {
            CompanyButton.OnCompanyUnlocked += CompanyUnlocked;
        }

        public void Settings(int index)
        {
            _settingsAnimator.SetInteger("Settings", index);
        }

        public void Information(int index)
        {
            _informationAnimator.SetInteger("Information", index);
        }

        public void SelectionView(int index)
        {
            _selectionAnimator.SetInteger("Information", index);
        }

        public void EducationSelectionView(int index)
        {
            _educationSelectionAnimator.SetInteger("Information", index);
        }

        public void CompanyUnlocked(int index)
        {
            _companyUnlockedAnimator.SetInteger("Information", index);
        }

        public void CompanyLocked(int index)
        {
            _companyLockedAnimator.SetInteger("CompanyInfo", index);
        }

        public void Info(int index)
        {
            _infoAnimator.SetInteger("Information", index);
        }

        public void Shop(int index)
        {
            _shopAnimator.SetInteger("Information", index);
        }

        public void GamePlaySurvival(int index)
        {
            _gamePlaySurvival.SetInteger("Information", index);
        }

        public void GamePlayCompany(int index)
        {
            _gamePlayCompany.SetInteger("Information", index);
        }

        private void OnDestroy()
        {
            CompanyButton.OnCompanyUnlocked -= CompanyUnlocked;
        }
    }
}