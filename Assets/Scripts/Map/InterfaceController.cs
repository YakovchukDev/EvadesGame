using Map;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour
{
    [SerializeField] private SoundManager _soundManager;
    [SerializeField] private GameObject _pauseButton;
    [SerializeField] private GameObject _pausePanel;
    public void OnPause()
    {
        UnityEngine.Time.timeScale = 0;
        _soundManager.PauseMusic();
        _pauseButton.SetActive(false);
        _pausePanel.SetActive(true);
    }
    public void OffPause()
    {
        UnityEngine.Time.timeScale = 1;
        _soundManager.PlayMusic();
        _pauseButton.SetActive(true);
        _pausePanel.SetActive(false);
    }
    public void Save()
    {
        MapManager.MainDataCollector.SaveCoins();
        SceneManager.LoadScene(0);   
    }

    public void FinishedExit()
    {
        SceneManager.LoadScene(0);
    }
}
