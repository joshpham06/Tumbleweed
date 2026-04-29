using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }
    public AudioSource backgroundMusic;
    public AudioSource playerSFX;
    public AudioSource worldSFX;

    public AudioClip mainMenuMusic;
    public AudioClip level1Music;
    public AudioClip level2Music;
    public AudioClip level3Music;

    public AudioClip collect;

    public AudioClip win;
    public AudioClip lose;

    public AudioClip uiClick;

    public Levels currentLevel;

    public enum Levels
    {
        One,
        Two,
        Three,
        MainMenu
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    private void Start()
    {
        AssignMusic();
    }

    private void AssignMusic()
    {
        switch (currentLevel)
        {
            case Levels.MainMenu:
                PlayBackgroundMusic(mainMenuMusic);
                break;
            case Levels.One:
                PlayBackgroundMusic(level1Music);
                break;
            case Levels.Two:
                PlayBackgroundMusic(level2Music);
                break;
            case Levels.Three:
                PlayBackgroundMusic(level3Music);
                break;
        }
    }

    private void PlayBackgroundMusic(AudioClip clip)
    {
        backgroundMusic.PlayOneShot(clip);
    }

    public void PlayCollectSFX()
    {
        playerSFX.PlayOneShot(collect);
    }

    public void PlayWinSFX()
    {
        worldSFX.PlayOneShot(win);
        backgroundMusic.Stop();
    }

    public void PlayLoseSFX()
    {
        worldSFX.PlayOneShot(lose);
        backgroundMusic.Stop();
    }

    public void PlayUIClick()
    {
        playerSFX.PlayOneShot(uiClick);
    }
}
