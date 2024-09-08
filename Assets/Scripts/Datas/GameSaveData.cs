[System.Serializable]
public class GameSaveData
{
    public int Level;
    public int Coins;
    public int SelectedPlayerIndex;

    public bool[] IsPlayerUnlocked;
    public bool[] IsPlayerSelected;
    public int[] PlayerCurrentLevel;

    public bool Sound;
    public bool Vibration;
    public bool Music;

    public int EnemyAchievementIndex;
    public int EnemyKilledCount;
    public bool IsEnemyChallengeFinished;

    public int LevelIndex;
    public int LevelReached;
    public bool IsLevelChallengeFinished;

    public bool IsSkinAchievementButtonPressed;

    public bool IsNoAdsSubPurchased;

    public bool ShowConfirmAgePanel;

    public GameSaveData(GameDataManager gameDataManager)
    {
        Level = gameDataManager.Level;
        Coins = gameDataManager.Coins;
        SelectedPlayerIndex = gameDataManager.SelectedPlayerIndex;
        
        IsPlayerUnlocked = new bool[gameDataManager.IsPlayerUnlocked.Length];
        IsPlayerSelected = new bool[gameDataManager.IsPlayerSelected.Length];
        PlayerCurrentLevel = new int[gameDataManager.PlayerCurrentLevel.Length];

        Sound = gameDataManager.Sound;
        Vibration = gameDataManager.Vibration;
        Music = gameDataManager.Music;

        EnemyAchievementIndex = gameDataManager.EnemyAchievementIndex;
        EnemyKilledCount = gameDataManager.EnemyKilledCount;
        IsEnemyChallengeFinished = gameDataManager.IsEnemyChallengeFinished;

        LevelIndex = gameDataManager.LevelIndex;
        LevelReached = gameDataManager.LevelReached;
        IsLevelChallengeFinished = gameDataManager.IsLevelChallengeFinished;

        IsNoAdsSubPurchased = gameDataManager.IsNoAdsSubPurchased;

        IsSkinAchievementButtonPressed = gameDataManager.IsSkinAchievementButtonPressed;

        ShowConfirmAgePanel = gameDataManager.ShowConfirmAgePanel;
        
        for (int i = 0 ; i < IsPlayerUnlocked.Length ; i++)
        {
            IsPlayerUnlocked[i] = gameDataManager.IsPlayerUnlocked[i];
        }
        
        for (int i = 0 ; i < IsPlayerSelected.Length ; i++)
        {
            IsPlayerSelected[i] = gameDataManager.IsPlayerSelected[i];
        }

        for (int i = 0 ; i < PlayerCurrentLevel.Length ; i++)
        {
            PlayerCurrentLevel[i] = gameDataManager.PlayerCurrentLevel[i];
        }
    }
}
