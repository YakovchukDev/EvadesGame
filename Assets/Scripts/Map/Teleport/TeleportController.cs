using System;
using Audio;
using UnityEngine;

namespace Map.Teleport
{
    public class TeleportController : MonoBehaviour
    {
        [SerializeField] private GameObject _teleportPanel;
        private AudioManager _audioManager;
        public static event Action OnTeleport;

        private void Start()
        {
            _audioManager=AudioManager.Instanse;
        }

        private void OnEnable()
        {
            TeleportControl.OpenTeleportMenu += SetActivaTeleportPanel;
        }
        private void OnDisable()
        {
            TeleportControl.OpenTeleportMenu -= SetActivaTeleportPanel;
        }
        public void SetActivaTeleportPanel(bool isEnter)
        {
            _teleportPanel.gameObject.SetActive(isEnter); 
        }
        public void ActivateTeleport()
        {
            _audioManager.Play("Friction");
            OnTeleport();
        }
    }
}
