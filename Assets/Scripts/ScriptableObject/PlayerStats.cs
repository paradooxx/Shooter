using UnityEngine;

[CreateAssetMenu(fileName = "PlayerStats", menuName = "Player/PlayerStats", order = 1)]
public class PlayerStats : ScriptableObject
{
    public int health { get; private set; }
    public float fireRate { get; private set; }

    [SerializeField] private int[] Health;
    [SerializeField] private float[] FireRate;

    public int maxLevel  { get ; private set; } = 5;
    public int currentLevel = 1;

    public bool isUnlocked;
    public bool isSelected;

    public int initialPrice;
    public int[] upgradePrice = new int[5];

    private void OnEnable()
    {
        UpdateStats();
    }

    public void UpdateStats()
    {
        if (currentLevel >= 1 && currentLevel <= maxLevel)
        {
            health = Health[currentLevel - 1];
            fireRate = FireRate[currentLevel - 1];
        }
    }
}
