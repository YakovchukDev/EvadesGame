using Map.Coins;
using Map.Stars;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioSource _coinSound;
    [SerializeField] private AudioSource _starSound;
    private AudioSource _mainMusic;

    private void OnEnable()
    {
        CoinControl.PlaySound += CoinSoundPlay;
        StarController.PlaySound += StarSoundPlay;
    }
    private void OnDisable()
    {
        CoinControl.PlaySound -= CoinSoundPlay;
        StarController.PlaySound -= StarSoundPlay;
    }
    private void Start()
    {
        _mainMusic = GetComponent<AudioSource>();
    }
    public void PauseMusic()
    {
        _mainMusic.Pause();
    }

    public void PlayMusic()
    {
        _mainMusic.Play();
    }
    private void CoinSoundPlay()
    {
        _coinSound.Play();
    }
    private void StarSoundPlay()
    {
        _starSound.Play();   
    }
}
