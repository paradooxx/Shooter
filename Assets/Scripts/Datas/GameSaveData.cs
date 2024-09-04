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

    public int LevelIndex;
    public int LevelReached;

    public bool IsSkinAchievementButtonPressed;

    public bool IsNoAdsSubPurchased;

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

        LevelIndex = gameDataManager.LevelIndex;
        LevelReached = gameDataManager.LevelReached;

        IsNoAdsSubPurchased = gameDataManager.IsNoAdsSubPurchased;

        IsSkinAchievementButtonPressed = gameDataManager.IsSkinAchievementButtonPressed;
        
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
