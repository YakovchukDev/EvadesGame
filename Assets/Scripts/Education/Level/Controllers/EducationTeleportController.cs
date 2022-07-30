using System.Collections;
using DG.Tweening;
using GamePlay.Character;
using Joystick_Pack.Examples;
using UnityEngine;
using UnityEngine.UI;

namespace Education.Level.Controllers
{
    public class EducationTeleportController : MonoBehaviour
    {
        [SerializeField] private EducationExperienceController _educationExperienceController;
        [SerializeField] private HandController _handController;
        [SerializeField] private GameObject _audioListener2;
        [SerializeField] private GameObject _teleportThereParticle;
        [SerializeField] private GameObject _teleportHereParticle;
        [SerializeField] private CharacterSpawner _characterSpawner;
        [SerializeField] private GameObject _backGround;
        [SerializeField] private Button _forOffInteractable;
        [SerializeField] private Image _image;
        [SerializeField] private Transform _upgradeHandPoint;

        private void OnEnable()
        {
            EducationTeleportControl.OpenTeleportMenu += SetTeleportButtonStatus;
            HealthController.OnEducationDie += TeleportAfterDie;
        }

        private void OnDisable()
        {
            EducationTeleportControl.OpenTeleportMenu -= SetTeleportButtonStatus;
            HealthController.OnEducationDie -= TeleportAfterDie;
        }

        private void Start()
        {
            _image.DOFade(0, 0);
        }

        private void SetTeleportButtonStatus(bool status)
        {
            if (status)
            {
                ShowTeleportButton();
            }
            else
            {
                HideTeleportButton();
            }
        }

        private void TeleportAfterDie()
        {
            if (_characterSpawner.Character.transform.position.x < 23)
            {
                _characterSpawner.Character.transform.position = new Vector3(-30, 1, 0);
            }
            else
            {
                _characterSpawner.Character.transform.position = new Vector3(30, 1, 0);
            }
        }

        public void TeleportButtonClick()
        {
            StartCoroutine(TeleportToCentralRoom());
            HideTeleportButton();
            JoystickPlayerExample.Speed = 10;

            if (_educationExperienceController.QuantityPoints > 0)
            {
                _backGround.SetActive(true);
                _handController.MoveHand(_upgradeHandPoint.position, 0.7f);
                _forOffInteractable.interactable = false;
            }
        }


        private IEnumerator TeleportToCentralRoom()
        {
            GameObject character = _characterSpawner.Character;
            _teleportThereParticle.transform.position = character.transform.position;
            _teleportThereParticle.SetActive(true);

            character.SetActive(false);
            _audioListener2.SetActive(true);

            yield return new WaitForSeconds(1);
            character.transform.position = new Vector3(30, 1, 0);

            _teleportHereParticle.transform.position = character.transform.position;
            _teleportHereParticle.SetActive(true);
            yield return new WaitForSeconds(1);
            _audioListener2.SetActive(false);
            character.SetActive(true);
        }

        private void ShowTeleportButton()
        {
            _image.DOFade(1, 0.6f);
            _image.rectTransform.DOAnchorPosX(30, 0.6f);
        }

        private void HideTeleportButton()
        {
            _image.DOFade(0, 0.6f);
            _image.rectTransform.DOAnchorPosX(_image.rectTransform.sizeDelta.x * -1, 0.6f);
        }
    }
}