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
        [SerializeField] private GameObject _educationSelectionClass;
        [SerializeField] private Animator _selectionAnimator;
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
            _menuEducationController.MoveHand(2);
            _educationSelectionClass.SetActive(true);
            _selectionAnimator.SetInteger("Information", 0);
        }
    }
}