using UnityEngine;

[CreateAssetMenu(fileName = "AchievementData", menuName = "Achievement/AchvData", order = 1)]
public class AchievementData : ScriptableObject
{
    public int achievementCount;
    public int rewardAmount;
    public bool isCompleted;
}
