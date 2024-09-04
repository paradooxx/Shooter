using UnityEditor;
using UnityEngine;

public class SFXManager : MonoBehaviour
{
    public static SFXManager Instance;

    [SerializeField] private AudioSource soundObject;
    
    [Space]
    [Header("SoundClips")]
    [SerializeField] private AudioClip explosionClip;
    [SerializeField] private AudioClip hammerClip;
    [SerializeField] private AudioClip coinClip;
    [SerializeField] private AudioClip gameWinClip;
    [SerializeField] private AudioClip gameLooseClip;
    [SerializeField] private AudioClip footStepClip;
    [SerializeField] private AudioClip barrelDestroyClip;
    [SerializeField] private AudioClip summonClip;
    [SerializeField] private AudioClip arrowClip;
    [SerializeField] private AudioClip gunClip;
    [SerializeField] private AudioClip swordClip;
    [SerializeField] private AudioClip spearClip;
    [SerializeField] private AudioClip fireClip;
    [SerializeField] private AudioClip buttonClip;
    [SerializeField] private AudioClip errorClip;
    [SerializeField] private AudioClip bossClip;
    [SerializeField] private AudioClip electricClip;

    private AudioSource mainBgAudio;

    [Space]
    [Header("PlayStatus")]
    public bool sound = false;
    public bool vibrate = true;
    public bool music = false;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        mainBgAudio = GetComponent<AudioSource>();
    }

    public void PlaySoundClip(AudioClip audioClip, Transform transform, float volume = 1f)
    {
        if (audioClip == null || !sound) return;

        AudioSource audioSource = Instantiate(soundObject, transform.position, Quaternion.identity);    
        audioSource.clip = audioClip;
        audioSource.volume = volume;
        audioSource.Play();

        Destroy(audioSource.gameObject, audioClip.length);
    }

    public void PlaySound(SoundType soundType, Transform transform, float volume = 1f)
    {
        AudioClip clipToPlay = null;
        switch (soundType)
        {
            case SoundType.Explosion:
                clipToPlay = explosionClip;
                break;
            case SoundType.Hammer:
                clipToPlay = hammerClip;
                break;
            case SoundType.Coin:
                clipToPlay = coinClip;
                break;
            case SoundType.GameWin:
                clipToPlay = gameWinClip;
                break;
            case SoundType.GameLoose:
                clipToPlay = gameLooseClip;
                break;
            case SoundType.FootStep:
                clipToPlay = footStepClip;
                break;
            case SoundType.BarrelDestroy:
                clipToPlay = barrelDestroyClip;
                break;
            case SoundType.Summon:
                clipToPlay = summonClip;
                break;
            case SoundType.Gun:
                clipToPlay = gunClip;
                break;
            case SoundType.Arrow:
                clipToPlay = arrowClip;
                break;
            case SoundType.Sword:
                clipToPlay = swordClip;
                break;
            case SoundType.Spear:
                clipToPlay = spearClip;
                break;
            case SoundType.Fire:
                clipToPlay = fireClip;
                break;
            case SoundType.Button:
                clipToPlay = buttonClip;
                break;
            case SoundType.Error:
                clipToPlay = errorClip;
                break;
            case SoundType.Boss:
                clipToPlay = bossClip;
                break;
            case SoundType.Electric:
                clipToPlay = electricClip;
                break;
        }

        PlaySoundClip(clipToPlay, transform, volume);
    }

    public void Vibrate()
    {
        if (vibrate)
            Handheld.Vibrate();
    }
}

public enum SoundType
{
    Explosion,
    Hammer,
    Coin,
    GameWin,
    GameLoose,
    FootStep,
    BarrelDestroy,
    Summon,
    Gun,
    Arrow,
    Sword,
    Spear,
    Fire,
    Button,
    Error,
    Boss,
    Electric
}
