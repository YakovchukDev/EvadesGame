using System;
using System.Collections;
using System.Collections.Generic;
using Audio;
using GamePlay.Character.Spell;
using Joystick_Pack.Examples;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Education.Level.Controllers;
using Education.Level.Controls;
using GamePlay.Map;
using GamePlay.Map.Expirience;

namespace GamePlay.Character
{
    public class CharacterUpdate : MonoBehaviour
    {
        public static int NumberSpell1Update;
        public static int NumberSpell2Update;
        public static bool CanSpell1Update;
        public static bool CanSpell2Update;
        public int NumberSpeedUpdate { get; private set; }
        public int NumberMaxManaUpdate { get; private set; }
        public int NumberManaRegenUpdate { get; private set; }
        [SerializeField] private Image _upgradeButton;
        [SerializeField] private GameObject _panel;
        [SerializeField] private Image _upgradeBlock;

        [SerializeField] private ManaController _manaController;

        [SerializeField] private List<UpgradeButton> _buttons;
        [SerializeField] private UpgradeButton _buttonSpeed;
        [SerializeField] private UpgradeButton _buttonMaxMana;
        [SerializeField] private UpgradeButton _buttonManaRegen;
        [SerializeField] private UpgradeButton _buttonSpell1;
        [SerializeField] private UpgradeButton _buttonSpell2;
        [SerializeField] private RectTransform _closeUpgradeBlockButton;
        private AudioManager _audioManager;
        private float _allMana;
        private int _quantityLevelPoints;

        public static Action<int> UseLevelPoints;

        private void Start()
        {
            _upgradeButton.DOFade(0, 0f);
            _upgradeBlock.DOFade(0, 0);
            _upgradeBlock.rectTransform.DOScale(Vector3.zero, 0);
            foreach (var button in _buttons)
            {
                button.GetComponent<RectTransform>().DOScale(Vector3.zero, 0);
            }

            _closeUpgradeBlockButton.DOScale(Vector3.zero, 0);

            NumberSpeedUpdate = 1;
            NumberMaxManaUpdate = 1;
            NumberManaRegenUpdate = 1;
            NumberSpell1Update = 1;
            NumberSpell2Update = 1;
            CanSpell1Update = false;
            CanSpell2Update = false;
            _audioManager = AudioManager.Instanse;
        }

        private void OnEnable()
        {
            ExpirienceController.UpdateUpgradeMenu += SetUpgradeButtonStatus;
            ExpirienceControl.OpenBoostMenu += SetUpgradeButtonStatus;

            EducationExperienceController.UpdateUpgradeMenu += SetUpgradeButtonStatus;
            EducationExperienceControl.OpenBoostMenu += SetUpgradeButtonStatus;

            CharacterSpawner.HandOverManaController += SetManaController;
            CharacterSpawner.InitializeUpgradePanel += Initialize;
           EducationSpawnCharacter.HandOverManaController += SetManaController;
           EducationSpawnCharacter.InitializeUpgradePanel += Initialize;
            ExpirienceController.OnGetLevelPoints += SetLevelPoints;

            EducationExperienceController.OnGetLevelPoints += SetLevelPoints;

            SpellChecker.OnRemoveSpell1 += RemoveSpell1;
            SpellChecker.OnRemoveSpell2 += RemoveSpell2;
        }

        private void OnDisable()
        {
            ExpirienceController.UpdateUpgradeMenu -= SetUpgradeButtonStatus;
            ExpirienceControl.OpenBoostMenu -= SetUpgradeButtonStatus;

            EducationExperienceController.UpdateUpgradeMenu -= SetUpgradeButtonStatus;
            EducationExperienceControl.OpenBoostMenu -= SetUpgradeButtonStatus;

            CharacterSpawner.HandOverManaController -= SetManaController;
            CharacterSpawner.InitializeUpgradePanel -= Initialize;
            EducationSpawnCharacter.HandOverManaController -= SetManaController;
            EducationSpawnCharacter.InitializeUpgradePanel -= Initialize;
            ExpirienceController.OnGetLevelPoints -= SetLevelPoints;

            EducationExperienceController.OnGetLevelPoints -= SetLevelPoints;

            SpellChecker.OnRemoveSpell1 -= RemoveSpell1;
            SpellChecker.OnRemoveSpell2 -= RemoveSpell2;
        }

        private void RemoveSpell1()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (_buttons[i].Type == UpgradeButtonEnum.Spell1)
                {
                    Destroy(_buttons[i].gameObject);
                    _buttons.RemoveAt(i);
                    i--;
                }
            }
        }

        private void RemoveSpell2()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (_buttons[i].Type == UpgradeButtonEnum.Spell2)
                {
                    Destroy(_buttons[i].gameObject);
                    _buttons.RemoveAt(i);
                    i--;
                }
            }
        }

        private void SetLevelPoints(int value)
        {
            _quantityLevelPoints = value;
        }

        private void SetManaController(ManaController manaController)
        {
            _manaController = manaController;
        }

        private void Initialize()
        {
            for (int i = 0; i < _buttons.Count; i++)
            {
                if (_buttons[i].Type == UpgradeButtonEnum.None || _manaController == null && _buttons[i].IsNeedMana)
                {
                    Destroy(_buttons[i].gameObject);
                    _buttons.RemoveAt(i);
                    i--;
                }
            }
        }

        public void SpeedUpdate()
        {
            if (_quantityLevelPoints >= NumberSpeedUpdate)
            {
                if (NumberSpeedUpdate <= 5)
                {
                    UseLevelPoints?.Invoke(NumberSpeedUpdate);
                    JoystickPlayerExample.Speed += 2;
                    NumberSpeedUpdate++;
                    _audioManager.Play("PressButton");
                    _buttonSpeed.GetText.text = NumberSpeedUpdate.ToString();
                    CheckActiveBoostMenu();
                }
            }
            else
            {
                ErrorUpgrade(_buttonSpeed);
            }
        }

        public void MaxManaUpdate()
        {
            if (_quantityLevelPoints >= NumberMaxManaUpdate)
            {
                if (NumberMaxManaUpdate <= 5)
                {
                    UseLevelPoints?.Invoke(NumberMaxManaUpdate);
                    _allMana = 100;
                    _allMana += 20 * NumberMaxManaUpdate;
                    ManaController.AllMana = _allMana;
                    NumberMaxManaUpdate++;
                    _audioManager.Play("PressButton");
                    _buttonMaxMana.GetText.text = NumberMaxManaUpdate.ToString();
                    CheckActiveBoostMenu();
                }
            }
            else
            {
                ErrorUpgrade(_buttonMaxMana);
            }
        }

        public void ManaRegenUpdate()
        {
            if (_quantityLevelPoints >= NumberManaRegenUpdate)
            {
                if (NumberManaRegenUpdate <= 5)
                {
                    UseLevelPoints?.Invoke(NumberManaRegenUpdate);
                    ManaController.Regen += 0.2f;
                    NumberManaRegenUpdate++;
                    _audioManager.Play("PressButton");
                    _buttonManaRegen.GetText.text = NumberManaRegenUpdate.ToString();
                    CheckActiveBoostMenu();
                }
            }
            else
            {
                ErrorUpgrade(_buttonManaRegen);
            }
        }

        public void Spell1Update()
        {
            if (_quantityLevelPoints >= NumberSpell1Update)
            {
                if (NumberSpell1Update <= 5)
                {
                    UseLevelPoints?.Invoke(NumberSpell1Update);
                    NumberSpell1Update++;
                    CanSpell1Update = true;
                    _audioManager.Play("PressButton");
                    _buttonSpell1.GetText.text = NumberSpell1Update.ToString();
                    CheckActiveBoostMenu();
                }
            }
            else
            {
                ErrorUpgrade(_buttonSpell1);
            }
        }

        public void Spell2Update()
        {
            if (_quantityLevelPoints >= NumberSpell2Update)
            {
                if (NumberSpell2Update <= 5)
                {
                    UseLevelPoints?.Invoke(NumberSpell2Update);
                    NumberSpell2Update++;
                    CanSpell2Update = true;
                    _audioManager.Play("PressButton");
                    _buttonSpell2.GetText.text = NumberSpell2Update.ToString();

                    CheckActiveBoostMenu();
                }
            }
            else
            {
                ErrorUpgrade(_buttonSpell2);
            }
        }

        private void ErrorUpgrade(UpgradeButton button)
        {
            _audioManager.Play("LittleExperience");
            var sequence = DOTween.Sequence();

            sequence.Append(button.ImageButton.DOColor(new Color(1, 0, 0), 0.5f));
            sequence.Append(button.ImageButton.DOColor(new Color(1, 1, 1), 0.5f));
        }

        public void SetUpgradeButtonStatus(bool isActive)
        {
            if (_quantityLevelPoints > 0 && isActive)
            {
                _upgradeButton.DOFade(1, 0.6f);
                _upgradeButton.rectTransform.DOAnchorPosX(-30, 0.6f);
            }
            else
            {
                _upgradeButton.DOFade(0, 0.6f);
                _upgradeButton.rectTransform.DOAnchorPosX(_upgradeButton.rectTransform.sizeDelta.x, 0.6f);
            }
        }

        public void SetUpgradeBlockStatus(bool isActive)
        {
            switch (isActive)
            {
                case true:
                {
                    StartCoroutine(ShowUpgradeBlock(0.8f));
                    break;
                }
                case false:
                {
                    StartCoroutine(HideUpgradeBlock());
                    break;
                }
            }
        }

        private void CheckActiveBoostMenu()
        {
            if (_quantityLevelPoints <= 0)
            {
                StartCoroutine(HideUpgradeBlock());
            }
        }

        private IEnumerator ShowUpgradeBlock(float duration)
        {
            _panel.SetActive(true);
            _upgradeBlock.DOFade(85f / 255f, duration);
            _upgradeBlock.rectTransform.DOScale(Vector3.one, duration);
            yield return new WaitForSeconds(0.5f);
            foreach (var button in _buttons)
            {
                button.GetComponent<RectTransform>().DOScale(Vector3.one, duration);
                yield return new WaitForSeconds(0.1f);
            }

            yield return new WaitForSeconds(0.1f);
            _closeUpgradeBlockButton.DOScale(Vector3.one, duration);
        }

        private IEnumerator HideUpgradeBlock()
        {
            _closeUpgradeBlockButton.DOScale(Vector3.zero, 0.2f);
            yield return new WaitForSeconds(0.2f);
            _upgradeBlock.DOFade(0, 0.5f);
            _upgradeBlock.rectTransform.DOScale(Vector3.zero, 0.5f);
            yield return new WaitForSeconds(0.2f);
            foreach (var button in _buttons)
            {
                button.GetComponent<RectTransform>().DOScale(Vector3.zero, 0f);
            }

            _panel.SetActive(false);
        }
    }
}