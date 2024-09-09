using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerChoice : MonoBehaviour
{
    [SerializeField] private GameObject[] players;
    [SerializeField] private PlayerStats[] playerStats;
    [SerializeField] private Button[] buttonPlayer,
                                      buttonUpgradePlayer;
    [SerializeField] private Transform instantiatePoint;
    [SerializeField] private TMP_Text[] selectText,
                                        statusText,
                                        levelTexts,
                                        priceTexts;
    
    [SerializeField] private PlayerPanelUpdate playerPanelUpdate;
    [SerializeField] private GameObject lockedMessage,
                                        notEnoughCoinMessage;

    [SerializeField] private GameObject playerPlatform;
    [SerializeField] private GameObject doubleDmg;
    [SerializeField] private SkinUnlockedAchievement skinAchievement;
    [SerializeField] private PlayerUIDisplay playerUIDisplay;
    private GameObject selectedPlayerPrefab,
                       currentSelectedPlayer;

    public static PlayerChoice Instance;
    private GameDataManager gameDataManager;
    private bool doublePlayer,
                 doubleDamage;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        gameDataManager = GameDataManager.Instance;

        selectedPlayerPrefab = players[gameDataManager.SelectedPlayerIndex];
        currentSelectedPlayer = players[gameDataManager.SelectedPlayerIndex];

        //assiging button functionalities
        for (int i = 0; i < buttonPlayer.Length; i++)
        {
            int index = i;
            buttonPlayer[index].onClick.AddListener(() => SelectPlayer(players[index], playerStats[index], index));
            buttonUpgradePlayer[index].onClick.AddListener(() => UpgradePlayer(playerStats[index], statusText[index], levelTexts[index], priceTexts[index], index));
        }
        UpdatePlayerData(playerStats, gameDataManager.IsPlayerUnlocked, gameDataManager.IsPlayerSelected, gameDataManager.PlayerCurrentLevel);
    }

    private void UpdatePlayerData(PlayerStats[] playerStats, bool[] arrayA, bool[] arrayB, int[] arrayC)
    {
        if (gameDataManager != null)
        {
            if (arrayA != null && arrayB != null && arrayC !=null && arrayA.Length == arrayB.Length && arrayB.Length == playerStats.Length && arrayC.Length == playerStats.Length)
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

    private void SelectPlayer(GameObject playerPrefab, PlayerStats playerStats,  int index)
    {
        if(playerStats.isUnlocked)
        {
            selectedPlayerPrefab = playerPrefab;
            gameDataManager.SelectedPlayerIndex = index;
            playerStats.isSelected = true;
            gameDataManager.IsPlayerSelected[index] = playerStats.isSelected;
            playerUIDisplay.ChangePlayerUIDisplay();
            for(int i = 0 ; i < this.playerStats.Length ; i++)
            {
                if(i != index)
                {
                    this.playerStats[i].isSelected = false;
                    gameDataManager.IsPlayerSelected[i] = false;
                }
            }
            
            playerPanelUpdate.UpdatePlayerSelection(this.playerStats, selectText);
            SFXManager.Instance.PlaySound(SoundType.Button, transform);
        }
        else
        {
            selectedPlayerPrefab = currentSelectedPlayer;
            StartCoroutine(ShowError(lockedMessage));
            SFXManager.Instance.PlaySound(SoundType.Error, transform);
        }
        gameDataManager.SaveGameData();
        
    }

    private void UpgradePlayer(PlayerStats playerStats, TMP_Text statusText, TMP_Text levelText, TMP_Text priceText, int index)
    {
        if(playerStats.isUnlocked && playerStats.currentLevel != 5 && playerStats.upgradePrice[playerStats.currentLevel] < Coins.coins)
        {
            if (playerStats.currentLevel == 4)
            {
                statusText.text = "Completed";
                statusText.color = Color.green;
                Coins.UpdateUIandCoin(playerStats.upgradePrice[playerStats.currentLevel], false);
                playerStats.currentLevel ++;
                gameDataManager.PlayerCurrentLevel[index] ++; 
                playerStats.UpdateStats();
                levelText.text = "Level : " + playerStats.currentLevel;
                playerPanelUpdate.UpdatePlayerUI(playerStats, statusText, levelText, priceText);
                gameDataManager.SaveGameData(); 
            } 
            else if(playerStats.currentLevel < 5 && playerStats.currentLevel != 4)
            {
                Debug.Log("Upgraded");
                Coins.UpdateUIandCoin(playerStats.upgradePrice[playerStats.currentLevel], false);
                playerStats.currentLevel ++;
                gameDataManager.PlayerCurrentLevel[index] ++;
                playerStats.UpdateStats();
                statusText.text = "Upgrade";
                statusText.color = Color.white;
                levelText.text = "Level : " + playerStats.currentLevel;
                playerPanelUpdate.UpdatePlayerUI(playerStats, statusText, levelText, priceText);
                gameDataManager.SaveGameData(); 
            }
            else
            {
                statusText.text = "Completed";
                statusText.color = Color.green;
                StartCoroutine(ShowError(notEnoughCoinMessage));
            }
            SFXManager.Instance.PlaySound(SoundType.Button, transform);
        }
        else if(!playerStats.isUnlocked && playerStats.initialPrice <= Coins.coins)
        {
            playerStats.isUnlocked = true;
            gameDataManager.IsPlayerUnlocked[index] = true;
            statusText.text = "Upgrade";
            statusText.color = Color.white;
            Coins.UpdateUIandCoin(playerStats.initialPrice, false);
            playerPanelUpdate.UpdatePlayerUI(playerStats, statusText, levelText, priceText);
            SFXManager.Instance.PlaySound(SoundType.Button, transform);
            skinAchievement.CheckSkinUnlock();
        }
        else if(playerStats.initialPrice > Coins.coins)
        {
            StartCoroutine(ShowError(notEnoughCoinMessage));
            SFXManager.Instance.PlaySound(SoundType.Error, transform);
        } 
        gameDataManager.SaveGameData();
        
    }

    public void StartGame()
    {
        if (selectedPlayerPrefab != null)
        {
            Instantiate(selectedPlayerPrefab, instantiatePoint).transform.parent = playerPlatform.transform;
        }

        if(doublePlayer)
        {
            DoublePlayerAtStart();
            doublePlayer = false;
        }

        if(doubleDamage)
        {
            StartCoroutine(DoubleDamage());
        }
    }

    public void DoublePlayerButton()
    {
        if(Coins.coins >= 250)
        {
            Coins.UpdateUIandCoin(250, false);
            doublePlayer = true;
            GameStateManager.Instance.SetState(GameState.MainMenu);
            SFXManager.Instance.PlaySound(SoundType.Button, transform);
        }
        else
        {
            StartCoroutine(ShowError(notEnoughCoinMessage));
            SFXManager.Instance.PlaySound(SoundType.Error, transform);
            return;
        }
    }

    public void DoubleDamageButton()
    {
        AdManager.Instance.ShowRewardedAd((isRewarded) => 
        {
            if(isRewarded)
            {
                doubleDamage = true;
                GameStateManager.Instance.SetState(GameState.MainMenu);
                SFXManager.Instance.PlaySound(SoundType.Button, transform);
                isRewarded = false;
            }
        });
    }

    public void DoublePlayerAtStart()
    {
        List<int> usedIndices = new List<int>();

        Instantiate(selectedPlayerPrefab, instantiatePoint).transform.parent = playerPlatform.transform;

        bool playerInstantiated = false;
        while (!playerInstantiated && usedIndices.Count < playerStats.Length)
        {
            int index = Random.Range(0, playerStats.Length);
            while (usedIndices.Contains(index))
            {
                index = Random.Range(0, playerStats.Length);
            }

            usedIndices.Add(index);

            if (playerStats[index].isUnlocked)
            {
                Instantiate(players[index], instantiatePoint.position + new Vector3(2, 0, 0), players[index].transform.rotation).transform.parent = playerPlatform.transform;

                playerInstantiated = true;
            }
        }

        if (!playerInstantiated)
        {
            return;
        }
    }

    public IEnumerator DoubleDamage()
    {
        Bullet.bulletDamageModifier = 2;
        doubleDmg.SetActive(true);
        yield return new WaitForSeconds(15);
        doubleDmg.SetActive(false);
        Bullet.bulletDamageModifier = 1;
    }

    private IEnumerator ShowError(GameObject obj)
    {
        obj.SetActive(true);
        yield return new WaitForSecondsRealtime(0.5f);
        obj.SetActive(false);
    }
}
