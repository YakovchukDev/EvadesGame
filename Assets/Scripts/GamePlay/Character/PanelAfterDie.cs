using GamePlay.Enemy.Spawner;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace GamePlay.Character
{
   public class PanelAfterDie : MonoBehaviour
   {
      public void RestartButton()
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
         Time.timeScale = 1;
         InterfaceController.Time = 0;
         InfinityEnemySpawner.SpawnNumber = 0;
      }
      public void ExitButton()
      {
         SceneManager.LoadScene("Menu");
         Time.timeScale = 1;
         InterfaceController.Time = 0;
         InfinityEnemySpawner.SpawnNumber = 0;
         InterfaceController.TimeSave();
      }
   
   }
}
