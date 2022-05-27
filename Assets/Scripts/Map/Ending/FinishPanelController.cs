using UnityEngine;
using Map.Data;

namespace Map
{
    public class FinishPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject _button;
        [SerializeField] private ResultController _resultPanel;
        
        private void OnEnable()
        {
            LevelComplited.OpenFinishMenu += SetActiveButton;
        }
        private void OnDisable()
        {
            LevelComplited.OpenFinishMenu -= SetActiveButton;
        }
        public void SetActiveButton(bool isEnter)
        {
            _button.gameObject.SetActive(isEnter); 
        }
        public void EndOfGame()
        {
            MapManager.MainDataCollector.Level.IsComplited = true;
            MapManager.MainDataCollector.SaveData();
            _resultPanel.gameObject.SetActive(true);
            _resultPanel.ShowResult();
        }
    }
}
