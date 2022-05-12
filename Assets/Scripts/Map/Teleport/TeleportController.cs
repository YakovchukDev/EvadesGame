using System;
using UnityEngine;

namespace Map.Teleport
{
    public class TeleportController : MonoBehaviour
    {
        [SerializeField] private GameObject _teleportPanel;
        public static event Action OnTeleport;
        
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
            OnTeleport();
        }
    }
}
