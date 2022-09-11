using System.Collections.Generic;
using DG.Tweening;
using GamePlay.Character;
using UnityEngine;
using UnityEngine.UI;

namespace Education.Level.Controllers
{
    public class HandController : MonoBehaviour
    {
        [SerializeField] private CharacterUpdate _characterUpdate;
        [SerializeField] private EducationExperienceController _educationExperienceController;
        [SerializeField] private GameObject _hand;
        [SerializeField] private GameObject _handParticle;
        [SerializeField] private Button _closeUpgradePanel;
        [SerializeField] private GameObject _textPanelUpgradePanel;
        [SerializeField] private List<Transform> _handPoints;
        private Transform _handTransform;
        private Image _handImage;

        public GameObject Hand => _hand;

        private void Start()
        {
            _handTransform = _hand.GetComponent<Transform>();
            _handImage = _hand.GetComponent<Image>();
            _handParticle.SetActive(false);
        }

        public void ForUpgradePanel(string number)
        {
            if (_educationExperienceController.QuantityPoints > 0 &&
                _characterUpdate.NumberMaxManaUpdate < _educationExperienceController.QuantityPoints&&
                _characterUpdate.NumberSpeedUpdate < _educationExperienceController.QuantityPoints&&
                _characterUpdate.NumberManaRegenUpdate < _educationExperienceController.QuantityPoints&&
                CharacterUpdate.NumberSpell2Update < _educationExperienceController.QuantityPoints)
            {
                switch (number)
                {
                    case "MaxMana":
                        if (_characterUpdate.NumberMaxManaUpdate < 6)
                        {
                            MoveHand(_handPoints[1].position, 1);
                        }
                        else
                        {
                            ForUpgradePanel("Speed");
                        }

                        break;
                    case "Speed":
                        if (_characterUpdate.NumberSpeedUpdate < 6)
                        {
                            MoveHand(_handPoints[2].position, 1);
                        }
                        else
                        {
                            ForUpgradePanel("RegenMana");
                        }

                        break;
                    case "RegenMana":
                        if (_characterUpdate.NumberManaRegenUpdate < 6)
                        {
                            MoveHand(_handPoints[3].position, 1);
                        }
                        else
                        {
                            ForUpgradePanel("Spell2");
                        }

                        break;
                    case "Spell2":
                        if (CharacterUpdate.NumberSpell2Update < 6)
                        {
                            MoveHand(_handPoints[4].position, 1);
                        }
                        else
                        {
                            ForUpgradePanel("MaxMana");
                        }

                        break;
                }
            }

            else
            {
                _hand.SetActive(false);
                _textPanelUpgradePanel.SetActive(false);
            }

            if (_educationExperienceController.QuantityPoints < 17 && _educationExperienceController.QuantityPoints > 0)
            {
                _closeUpgradePanel.interactable = true;
                print(1);
            }
        }

        public void MoveHand(Vector3 endPosition, float duration)
        {
            _handTransform.position = _handPoints[0].position;
            _handImage.color = new Color(1, 1, 1, 0);
            _hand.SetActive(true);
            _handImage.DOColor(new Color(1, 1, 1, 1), duration);
            _handTransform.DOMove(endPosition, duration).OnComplete(TurnOnParticle);
        }

        private void TurnOnParticle()
        {
            _handParticle.SetActive(true);
        }
    }
}