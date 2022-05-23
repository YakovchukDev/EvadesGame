using GamePlay.Enemy.Spawner;
using Menu.SelectionClass;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;

public class LevelsInterfaceController : SelectionClassView
{
    [SerializeField] private AudioMixerGroup _audioMixer;

    private static readonly string[] CharacterObject =
    {
        "JustTime", "NecroTime", "MorfeTime", "NeoTime",
        "InvulnerableTime", "NexusTime"
    };

    public void OnPause()
    {
        Time.timeScale = 0;
        _audioMixer.audioMixer.SetFloat("EffectVolume", -80);
    }

    public void OffPause()
    {
        Time.timeScale = 1;
        _audioMixer.audioMixer.SetFloat("EffectVolume", 0);
    }

    public void ExitButton()
    {
        SceneManager.LoadScene("Menu");

        Time.timeScale = 1;
        InfinityEnemySpawner.SpawnNumber = 0;
        _audioMixer.audioMixer.SetFloat("EffectVolume", 0);
    }
}