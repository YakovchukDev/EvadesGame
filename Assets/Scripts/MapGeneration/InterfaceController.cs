using GamePlay.Enemy.Spawner;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InterfaceController : MonoBehaviour
{
    public void OnPause()
    {
        Time.timeScale = 0;
    }

    public void OffPause()
    {
        Time.timeScale = 1;
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");
        Time.timeScale = 1;
        InfinityEnemySpawner.SpawnNumber = 0;
    }
}
