using UnityEngine;
using Map.Data;

namespace Map
{
    public class FinishPanelController : MonoBehaviour
    {
        [SerializeField] private GameObject _button;
        [SerializeField] private GameObject _resultPanel;
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
            _resultPanel.SetActive(true);
            GeneralParameters.MainDataCollector.Level.IsComplited = true;
            GeneralParameters.MainDataCollector.SaveData();
        }
    }
}
