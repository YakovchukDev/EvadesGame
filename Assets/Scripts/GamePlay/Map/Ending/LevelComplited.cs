using UnityEngine;

namespace Map.Ending
{
    public class LevelComplited : MonoBehaviour
    {
        public delegate void Finish(bool isEnter);
        private ResultController _resultController;

        public void SetResultController(ResultController resultController)
        {
            _resultController = resultController;
        }
        public void EndOfGame()
        {
            //Time.timeScale = 0;
            MapManager.MainDataCollector.Level.IsComplited = true;
            MapManager.MainDataCollector.SaveData();
            _resultController.gameObject.SetActive(true);
            StartCoroutine(_resultController.ShowResult());
        }
    }
}