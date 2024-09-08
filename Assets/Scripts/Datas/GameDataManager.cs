using UnityEngine;

public class GameDataManager : MonoBehaviour
{
    [Header("Level Data")]
    [SerializeField] private int level;
    [SerializeField] private int coins;

    [Header("Player Panel Data")]
    [SerializeField] private int selectedPlayerIndex;
    [SerializeField] private bool[] isPlayerUnlocked = new bool[5];
    [SerializeField] private bool[] isPlayerSelected = new bool[5];
    [SerializeField] private int[] playerCurrentLevel = new int[5];

    [Header("Settings Data")]
    [SerializeField] private bool sound;
    [SerializeField] private bool vibration;
    [SerializeField] private bool music;

    [Header("Enemy Achievement Data")]
    [SerializeField] private int enemyAchievementIndex;
    [SerializeField] private int enemyKilledCount;
    [SerializeField] private bool isEnemyChallengeFinished;

    [Header("Level Achievement Data")]
    [SerializeField] private int levelIndex;
    [SerializeField] private int levelReached;
    [SerializeField] private bool isLevelChallengeFinished;

    [Header("Skin Achievement Data")]
    [SerializeField] private bool isSkinAchievementButtonPressed;

    [Header("Ads Data")]
    [SerializeField] private bool isNoAdsSubPurchased;

    [Header("Other Datas")]
    [SerializeField] private bool showConfirmAgePanel;

    public static GameDataManager Instance;

    public int Level { get => level; set { level = value; SaveGameData(); }}

    public int Coins { get => coins; set { coins = value; SaveGameData(); }}

    public int SelectedPlayerIndex { get => selectedPlayerIndex; set { selectedPlayerIndex = value; SaveGameData(); }} 

    public bool Sound { get => sound; set { sound = value; SaveGameData(); }}

    public bool Vibration { get => vibration; set { vibration = value; SaveGameData(); }}

    public bool Music { get => music; set { music = value; SaveGameData(); }}

    public int EnemyAchievementIndex { get => enemyAchievementIndex; set { enemyAchievementIndex = value; SaveGameData(); }}  

    public int EnemyKilledCount { get => enemyKilledCount; set { enemyKilledCount = value ; SaveGameData(); }}

    public int LevelIndex { get => levelIndex;  set { levelIndex = value; SaveGameData(); }}

    public int LevelReached { get => levelReached; set { levelReached = value; SaveGameData(); }}

    public bool IsSkinAchievementButtonPressed { get => isSkinAchievementButtonPressed; set { isSkinAchievementButtonPressed = value; SaveGameData(); }}

    public bool IsNoAdsSubPurchased { get => isNoAdsSubPurchased; set { isNoAdsSubPurchased = value; SaveGameData(); }}

    public bool IsEnemyChallengeFinished { get => isEnemyChallengeFinished; set { isEnemyChallengeFinished = value; SaveGameData(); }} 

    public bool IsLevelChallengeFinished { get => isLevelChallengeFinished; set { isLevelChallengeFinished = value; SaveGameData(); }}

    public bool ShowConfirmAgePanel { get => showConfirmAgePanel; set { showConfirmAgePanel = value; SaveGameData(); }}

    public bool[] IsPlayerUnlocked 
    {
        get => isPlayerUnlocked; 
        set
        {
            if (value.Length == isPlayerUnlocked.Length)
            {
                isPlayerUnlocked = value;
                SaveGameData();
            }
            else
            {
                return;
            }
        }
    }

    public bool[] IsPlayerSelected
    {
        get => isPlayerSelected;
        set
        {
            if(value.Length == isPlayerSelected.Length)
            {
                isPlayerSelected = value;
                SaveGameData();
            }
            else
            {
                return;
            }
        }
    }

    public int[] PlayerCurrentLevel
    {
        get => playerCurrentLevel;
        set
        {
            if(value.Length == playerCurrentLevel.Length)
            {
                playerCurrentLevel = value;
                SaveGameData();
            }
            else
            {
                return;
            }
            
        }
    }

    private void Awake()
    {
        LoadGameData();
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        Application.targetFrameRate = 60;
    }

    private void LoadGameData()
    {
        GameSaveData gameSaveData = SaveSystem.LoadData();
        if (gameSaveData == null)   return;

        Level = gameSaveData.Level;
        Coins = gameSaveData.Coins;
        SelectedPlayerIndex = gameSaveData.SelectedPlayerIndex; 

        IsPlayerUnlocked = gameSaveData.IsPlayerUnlocked;
        IsPlayerSelected = gameSaveData.IsPlayerSelected;
        playerCurrentLevel = gameSaveData.PlayerCurrentLevel;
        
        Sound = gameSaveData.Sound;
        Vibration = gameSaveData.Vibration;
        Music = gameSaveData.Music;

        EnemyAchievementIndex = gameSaveData.EnemyAchievementIndex;
        EnemyKilledCount = gameSaveData.EnemyKilledCount;
        IsEnemyChallengeFinished = gameSaveData.IsEnemyChallengeFinished;

        LevelIndex = gameSaveData.LevelIndex;
        LevelReached = gameSaveData.LevelReached;
        IsLevelChallengeFinished = gameSaveData.IsLevelChallengeFinished;

        IsSkinAchievementButtonPressed = gameSaveData.IsSkinAchievementButtonPressed;

        IsNoAdsSubPurchased = gameSaveData.IsNoAdsSubPurchased;

        ShowConfirmAgePanel = gameSaveData.ShowConfirmAgePanel;
    }

    public void SaveGameData()
    {
        SaveSystem.SaveData(this);
    }
}
