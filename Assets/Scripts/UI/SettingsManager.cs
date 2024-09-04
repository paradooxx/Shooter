using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [SerializeField] private Sprite on;
    [SerializeField] private Sprite off;
    [SerializeField] private Button soundButton;
    [SerializeField] private Button vibrationButton;
    [SerializeField] private Button musicButton;

    private bool sound;
    private bool vibration;
    private bool music;

    private GameDataManager gameDataManager;
    private SFXManager sfxManager;

    private void OnEnable()
    {
        gameDataManager = GameDataManager.Instance;
        sfxManager = SFXManager.Instance;
    }

    private void Start()
    {
        UpdateSoundSprite();
        UpdateVibrateSprite();
        LoadSettings();
        ApplySettings();
        soundButton.onClick.AddListener(() => ToggleSound());
        vibrationButton.onClick.AddListener(() => ToggleVibration());
        musicButton.onClick.AddListener(() => ToggleMusic());
    }

    public void ToggleSound()
    {
        sound = !sound;
        sfxManager.sound = sound;
        UpdateSoundSprite();
        SaveSettings();
        SFXManager.Instance.PlaySound(SoundType.Button, transform);
    }

    public void ToggleVibration()
    {
        vibration = !vibration;
        sfxManager.vibrate = vibration;
        UpdateVibrateSprite();
        SaveSettings();
        SFXManager.Instance.PlaySound(SoundType.Button, transform);
    }

    public void ToggleMusic()
    {
        music = !music;
        sfxManager.music = music;
        UpdateMusicSprite();
        SaveSettings();
        SFXManager.Instance.PlaySound(SoundType.Button, transform);
    }

    private void UpdateSoundSprite()
    {
        soundButton.image.sprite = sound ? on : off;
    }

    private void UpdateVibrateSprite()
    {
        vibrationButton.image.sprite = vibration ? on : off;
    }

    private void UpdateMusicSprite()
    {
        musicButton.image.sprite = music ? on : off; 
    }

    private void SaveSettings()
    {
        gameDataManager.Sound = sound;
        gameDataManager.Vibration = vibration;
        gameDataManager.Music = music;
        gameDataManager.SaveGameData();
    }

    public void LoadSettings()
    {
        sound = gameDataManager.Sound;
        vibration = gameDataManager.Vibration;
        music = gameDataManager.Music;
        UpdateSoundSprite();
        UpdateVibrateSprite();
        UpdateMusicSprite();
    }

    public void ApplySettings()
    {
        sfxManager.sound = sound;
        sfxManager.vibrate = vibration;
        sfxManager.music = music;
    }
}
