using TMPro;
using UnityEngine;

public class PlayerPanelUpdate : MonoBehaviour
{
    [SerializeField] private TMP_Text[] selectText; 
    [SerializeField] private TMP_Text[] statusText;
    [SerializeField] private TMP_Text[] levelText;
    [SerializeField] private TMP_Text[] priceText;

    [SerializeField] private PlayerStats[] playerStats;
    [SerializeField] private GameObject coinImage;

    private GameDataManager gameDataManager;
    private Animator animator;

    private void Awake()
    {
        gameDataManager = GameDataManager.Instance;
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        UpdatePlayerData(playerStats, gameDataManager.IsPlayerUnlocked, gameDataManager.IsPlayerSelected, gameDataManager.PlayerCurrentLevel);
        UpdatePlayerSelection();
        UpdatePlayerUI();
    }

    private void UpdatePlayerData(PlayerStats[] playerStats, bool[] arrayA, bool[] arrayB, int[] arrayC)
    {
        if (gameDataManager != null)
        {
            if (arrayA != null && arrayB != null && arrayA.Length == arrayB.Length && arrayB.Length == playerStats.Length)
            {
                for (int i = 0; i < playerStats.Length; i++)
                {
                    playerStats[i].isUnlocked = arrayA[i];
                    playerStats[i].isSelected = arrayB[i];
                    playerStats[i].currentLevel = arrayC[i];
                }
            }
            else
            {
                return;
            }
        }
    }

    private void UpdatePlayerSelection()
    {
        for (int i = 0; i < selectText.Length; i++)
        {
            selectText[i].text = "Select";
        }

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].isSelected)
            {
                selectText[i].text = "Selected";
            }

            if (playerStats[i].isUnlocked)
            {
                statusText[i].text = "Upgrade";
            }
        }
    }

    public void UpdatePlayerSelection(PlayerStats[] playerStats, TMP_Text[] selectText)
    {
        for (int i = 0; i < selectText.Length; i++)
        {
            selectText[i].text = "Select";
        }

        for (int i = 0; i < playerStats.Length; i++)
        {
            if (playerStats[i].isSelected)
            {
                selectText[i].text = "Selected";
            }
        }
    }

    public void ResetSelection(TMP_Text[] selectText)
    {
        for (int i = 0; i < selectText.Length; i++)
        {
            selectText[i].text = "Select";
        }
    }
    
    private void UpdatePlayerUI()
    {
        for(int i = 0; i < playerStats.Length ; i++)
        {
            if(playerStats[i].isUnlocked)
            {
                if(playerStats[i].currentLevel == 5)
                {
                    statusText[i].text = "Completed";
                    Debug.Log("COMPLEEEEEETED");
                }
                else if(playerStats[i].currentLevel < 5 && playerStats[i].currentLevel != 1)
                {
                    statusText[i].text = "Upgrade";
                }

                if(playerStats[i].currentLevel == 5)
                {
                    priceText[i].text = "";
                    // statusText[i].text = "Upgraded";
                }
                else
                {
                    priceText[i].text = playerStats[i].upgradePrice[playerStats[i].currentLevel].ToString();
                }
                
            }
            else
            {
                priceText[i].text = playerStats[i].initialPrice.ToString();
            }
            levelText[i].text = "Level : " + playerStats[i].currentLevel;
        }
    }

    public void UpdatePlayerUI(PlayerStats playerStats, TMP_Text statusText, TMP_Text levelText, TMP_Text priceText) 
    {
        if(playerStats.isUnlocked)
        {
            if(playerStats.currentLevel == 5)
            {
                statusText.text = "Completed";
            }
            else if(playerStats.currentLevel < 5 && playerStats.currentLevel != 1)
            {
                statusText.text = "Upgrade";
            }

            if(playerStats.currentLevel == 5)
            {
                priceText.text = "";
            }
            else
            {
                priceText.text = playerStats.upgradePrice[playerStats.currentLevel].ToString();
            }
        }
        else
        {
            priceText.text = playerStats.initialPrice.ToString();
        }
        levelText.text = "Level : " + playerStats.currentLevel;   
    }
}
