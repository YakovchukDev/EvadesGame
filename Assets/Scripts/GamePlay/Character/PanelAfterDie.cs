using System;
using GamePlay.Enemy.ForInfinity.Spawner;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

namespace GamePlay.Character
{
   public class PanelAfterDie : MonoBehaviour
   {        
      [SerializeField] private AudioMixerGroup _audioMixer;
      public static event Action RestartLevel;
      public static event Action ExitToMenu;

      public void RestartButton()
      {
         Time.timeScale = 1;
         RestartLevel?.Invoke();
         InfinityEnemySpawner.SpawnNumber = 0;
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
      }
      public void ExitButton()
      {
         SceneManager.LoadScene("Menu");
         Time.timeScale = 1;
         ExitToMenu?.Invoke();
         InfinityEnemySpawner.SpawnNumber = 0;
         _audioMixer.audioMixer.SetFloat("ImportantVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("ImportantVolume")));
         _audioMixer.audioMixer.SetFloat("EffectVolume", Mathf.Lerp(-80, 0, PlayerPrefs.GetFloat("EffectVolume")));
      }
   
   }
}
