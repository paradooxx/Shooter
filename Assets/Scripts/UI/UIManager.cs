using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [Header("Panels")]
    [SerializeField] private GameObject gameWinPanel;
    [SerializeField] private GameObject gameLoosePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject selectPlayerPanel;
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject shopPanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private GameObject mainBgPanel;
    [SerializeField] private GameObject perksPanel;
    [SerializeField] private GameObject wheelPanel;
    [SerializeField] private GameObject tutorialPanel;
    [SerializeField] private GameObject achievementPanel;
    [SerializeField] private GameObject coinPanel;
    [SerializeField] private GameObject noAdsPanel;
    [SerializeField] private GameObject specialOfferPanel;
    private GameObject activePanel;
    private List<GameObject> unactivePanels;

    [Header("Buttons")]
    [SerializeField] private Button shopPlusButton;
    [SerializeField] private Button deleteButton;
    [SerializeField] private Button specialOfferButton;
                                        
    private void OnEnable() => GameStateManager.OnStateChange += ManagePanels;

    private void OnDisable() => GameStateManager.OnStateChange -= ManagePanels;

    private void Awake()
    {
        unactivePanels = new List<GameObject>{
                                              gameWinPanel, gameLoosePanel, settingsPanel, 
                                              selectPlayerPanel, mainMenuPanel, shopPanel,
                                              gamePanel, perksPanel, wheelPanel, 
                                              tutorialPanel, achievementPanel, noAdsPanel,
                                              specialOfferPanel
                                            };
        mainBgPanel.SetActive(true);
        HideAllPanel();

        deleteButton.onClick.AddListener(() => SaveSystem.DeleteSaveData());
    }

    private void Start()
    {
        if(LevelDataContainer.Instance.level % 3 == 0)
        {
            StartCoroutine(NoAdsCoroutine());
        }

        /* if(LevelDataContainer.Instance.level % 1 == 0)
        {
            specialOfferButton.gameObject.SetActive(true);
        }
        else
        {
            specialOfferButton.gameObject.SetActive(false);
        } */
    }

    private IEnumerator NoAdsCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.2f);
        ManagePanels(GameState.NoAds);
    }
    
    private void ManagePanels(GameState state)
    {
        switch (state)
        {
            case GameState.Win:
                StartCoroutine(SetWLPanel(gameWinPanel));
                shopPlusButton.interactable = false;
                Time.timeScale = 0f;
                break;
            case GameState.Loose:
                StartCoroutine(SetWLPanel(gameLoosePanel));
                shopPlusButton.interactable = false;
                Time.timeScale = 0f;
                break;
            case GameState.SelectPlayer:
                SetActivePanel(selectPlayerPanel);
                break;
            case GameState.Game:
                SetActivePanel(gamePanel);
                mainBgPanel.SetActive(false);
                if(LevelDataContainer.Instance.level == 1)
                {
                    tutorialPanel.SetActive(true);
                }
                else
                {
                    Time.timeScale = 1f;
                }
                break;
            case GameState.MainMenu:
                SetActivePanel(mainMenuPanel);
                AdManager.Instance.ShowBannerAd();
                shopPlusButton.interactable = true;
                break;
            case GameState.Settings:
                SetActivePanel(settingsPanel);
                break;
            case GameState.Shop:
                SetActivePanel(shopPanel);
                break;
            case GameState.Perks:
                SetActivePanel(perksPanel);
                break;
            case GameState.Wheel:
                SetActivePanel(wheelPanel);
                break;
            case GameState.Achievement:
                SetActivePanel(achievementPanel);
                break;
            case GameState.NoAds:
                SetActivePanel(noAdsPanel);
                mainMenuPanel.SetActive(true);
                break;
            case GameState.SpecialOffer:
                SetActivePanel(specialOfferPanel);
                break;
            case GameState.NoState:
                HideAllPanel();
                break;
        }

        if(GameStateManager.CurrentGameState == GameState.Game)
        {
            coinPanel.SetActive(false);
        }
        else if (GameStateManager.CurrentGameState == GameState.Win || GameStateManager.CurrentGameState == GameState.Loose)
        {
            StartCoroutine(CoinPanelCoroutine());
        }
        else
        {
            coinPanel.SetActive(true);
        }
    }

    private IEnumerator CoinPanelCoroutine()
    {
        yield return new WaitForSecondsRealtime(0.5f);
        coinPanel.SetActive(true);
    }

    private void SetActivePanel(GameObject panel)
    {
        DisableActivePanel();
        unactivePanels.Remove(panel);
        activePanel = panel;
        panel.SetActive(true);
    }

    private IEnumerator SetWLPanel(GameObject panel)
    {
        yield return new WaitForSecondsRealtime(0.8f);
        DisableActivePanel();
        unactivePanels.Remove(panel);
        activePanel = panel;
        panel.SetActive(true);
    }

    private void HideAllPanel()
    {
        DisableActivePanel();
        foreach (GameObject panel in unactivePanels)
        {
            panel.SetActive(false);
        }
    }

    private void DisableActivePanel()
    {
        if(activePanel == null) return;
        unactivePanels.Add(activePanel);
        activePanel.SetActive(false);
        activePanel = null;
    }
}
