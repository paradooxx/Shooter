using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    public Sprite redButton;
    public Sprite greenButton;
    public Sprite brownButton;

    [SerializeField] private GameObject notificationObject;

    public void NotificationUpdate()
    {
        if(LevelProgressionAchievements.isLevelReached || EnemyKillAchievements.isEnemyAchievementComplete || SkinUnlockedAchievement.isSkinUnlocked)
        {
            notificationObject.SetActive(true);
        }
        else if(!LevelProgressionAchievements.isLevelReached && !EnemyKillAchievements.isEnemyAchievementComplete && !SkinUnlockedAchievement.isSkinUnlocked)
        {
            notificationObject.SetActive(false);
        }
    }
}
