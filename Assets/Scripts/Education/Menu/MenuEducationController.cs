using System.Collections.Generic;
using DG.Tweening;
using Menu;
using UnityEngine;
using UnityEngine.UI;

namespace Education.Menu
{
    public class MenuEducationController : MonoBehaviour
    {
        [SerializeField] private GameObject _educationPanel;
        [SerializeField] private List<Button> _offButtons;
        [SerializeField] private GameObject _hand;
        [SerializeField] private ScrollRect _scrollRect;
        [SerializeField] private Transform[] _positionForHand;
        private GameObject _handParticle;
        private const float Speed = 1f;

        private void Start()
        {
            if (PlayerPrefs.GetInt("Education") == 0)
            {
                _educationPanel.SetActive(true);
            }
            else
            {
                enabled = false;
            }
        }

        public void DontStudy()
        {
            _educationPanel.SetActive(false);
            PlayerPrefs.SetInt("Education",1);
            enabled = false;
        }
        public void StartStudying()
        {
            _educationPanel.SetActive(false);
            _hand.GetComponent<Image>().DOFade(0, 0);
            _hand.SetActive(true);
            _hand.GetComponent<Image>().DOFade(1, 0.5f);
            _handParticle = _hand.transform.GetChild(0).gameObject;
            foreach (var button in _offButtons)
            {
                button.interactable = false;
            }

            _scrollRect.enabled = false;
            MoveHand(0);
            CompanyButton.OnEducation += MoveHand;
        }
        public void MoveHand(int doNumber)
        {
            try
            {
                _handParticle.SetActive(false);
                _hand.GetComponent<Transform>().DOMove(_positionForHand[doNumber].position, Speed).OnComplete(TurnOnParticle);
            }
            catch
            {
                print("Education complete.");
            }
        }

        public void OffHand()
        {
            try
            {
                _hand.SetActive(false);
            }
            catch
            {
                print("Education complete.");
            }
        }

        private void TurnOnParticle()
        {
            _handParticle.SetActive(true);
        }

        private void OnDisable()
        {
            CompanyButton.OnEducation -= MoveHand;
        }
    }
}