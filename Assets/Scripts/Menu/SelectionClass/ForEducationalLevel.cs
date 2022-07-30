using System.Collections.Generic;
using Audio;
using Education.Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Menu.SelectionClass
{
    public class ForEducationalLevel : MonoBehaviour

    {
        [SerializeField] private MenuEducationController _menuEducationController;
        [SerializeField] private SelectionClassView _selectionClassView;
        [SerializeField] private GameObject _selectionClass;
        [SerializeField] private Animator _selectionAnimator;
        [SerializeField] private CharacterInfo _characterInfo;
        [SerializeField] private Button _neededCharacterButton;
        [SerializeField] private List<Button> _characterButtons;
        [SerializeField] private List<Image> _stars;
        [SerializeField] private Sprite _openStarSprite;
        private AudioManager _audioManager;


        private void Start()
        {
            _audioManager = AudioManager.Instanse;
            for (int i = 0; i < PlayerPrefs.GetInt("EducationStarCount"); i++)
            {
                _stars[i].sprite = _openStarSprite;
            }
        }

        public void EducationLevelButton()
        {
            _audioManager.Play("PressButton");
            foreach (var characterButton in _characterButtons)
            {
                characterButton.interactable = false;
            }

            _neededCharacterButton.interactable = true;
            _menuEducationController.MoveHand(2);
            _selectionClassView.SetWhatPlaying("Education");
            switch (PlayerPrefs.GetString("Language"))
            {
                case "English":
                    _selectionClassView.InfoTime.text = "Level: Education";
                    break;
                case "Russian":
                    _selectionClassView.InfoTime.text = "Уровень: Обучение";
                    break;
                case "Ukrainian":
                    _selectionClassView.InfoTime.text = "Рівень: Навчання";
                    break;
            }

            _selectionClass.SetActive(true);
            _selectionAnimator.SetInteger("Information", 0);
            _selectionClassView.ChoiceTypeOfCharacter(PlayerPrefs.GetInt("SelectionNumber"));
            _characterInfo.SetAbilitiesAndClass(PlayerPrefs.GetInt("SelectionNumber"));
        }
    }
}