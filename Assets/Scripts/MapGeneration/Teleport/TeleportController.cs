using UnityEngine;

namespace Map
{
    public class TeleportController : MonoBehaviour
    {
        [SerializeField] private GameObject _teleportPanel;
        private void OnEnable()
        {
            TeleportControl.OpenTeleportMenu += SetActiveTeleportPanel;
        }
        private void OnDisable()
        {
            TeleportControl.OpenTeleportMenu -= SetActiveTeleportPanel;
        }
        public void SetActiveTeleportPanel(bool isEnter)
        {
            _teleportPanel.gameObject.SetActive(isEnter); 
        }
    }
}
