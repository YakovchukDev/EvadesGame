using UnityEngine;

namespace Map
{
    public class FinishPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject _finishPanel;
        [SerializeField] private GameObject _resultPanel;
        private void OnEnable()
        {
            LevelComplited.OpenFinishMenu += SetActiveFinishPanel;
        }
        private void OnDisable()
        {
            LevelComplited.OpenFinishMenu -= SetActiveFinishPanel;
        }
        public void SetActiveFinishPanel(bool isEnter)
        {
            _finishPanel.gameObject.SetActive(isEnter); 
        }
        public void EndOfGame()
        {
            _resultPanel.SetActive(true);
            GeneralParameters.MainDataCollector.Level.IsComplited = true;
            GeneralParameters.MainDataCollector.SaveData();
        }
    }
}
