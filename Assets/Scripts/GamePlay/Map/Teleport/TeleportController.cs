using System;
using Audio;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace Map.Teleport
{
    public class TeleportController : MonoBehaviour
    {
        private Image _image;
        public static event Action OnTeleport;

        private void OnEnable()
        {
            TeleportControl.OpenTeleportMenu += SetTeleportButtonStatus;
        }

        private void OnDisable()
        {
            TeleportControl.OpenTeleportMenu -= SetTeleportButtonStatus;
        }

        private void Start()
        {
            _image = GetComponent<Image>();
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

        public void TeleportButtonClick()
        {
            OnTeleport?.Invoke();
            HideTeleportButton();
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