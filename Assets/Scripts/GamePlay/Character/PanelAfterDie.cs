using GamePlay.Enemy.Spawner;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace GamePlay.Character
{
   public class PanelAfterDie : MonoBehaviour
   {        [SerializeField] private AudioMixerGroup _audioMixer;

      public void RestartButton()
      {
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
         Time.timeScale = 1;
         InfinityInterfaceController.Time = 0;
         InfinityEnemySpawner.SpawnNumber = 0;
      }
      public void ExitButton()
      {
         SceneManager.LoadScene("Menu");
         Time.timeScale = 1;
         InfinityInterfaceController.Time = 0;
         InfinityEnemySpawner.SpawnNumber = 0;
         InfinityInterfaceController.TimeSave();
         _audioMixer.audioMixer.SetFloat("ImportantVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("ImportantVolume")));
         _audioMixer.audioMixer.SetFloat("EffectVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("EffectVolume")));
      }
   
   }
}
