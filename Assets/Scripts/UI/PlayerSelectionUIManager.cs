using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSelectionUIManager : MonoBehaviour
{
    [SerializeField] private GameObject playerPanel;
    [SerializeField] private GameObject archerPanel;
    [SerializeField] private GameObject knightPanel;
    [SerializeField] private GameObject spearinPanel;
    [SerializeField] private GameObject wizardPanel;
    [SerializeField] private GameObject soldierPanel;
    
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;

    private GameObject activePlayerPanel;
    private List<GameObject> unactivePlayerPanels;
    private List<PlayerType> playerTypes;
    private int currentIndex;

    private void OnEnable() => PlayerSelectionManager.OnPlayerChanged += ManagePlayerPanels;

    private void OnDisable() => PlayerSelectionManager.OnPlayerChanged -= ManagePlayerPanels;
    
    private void Awake()
    {
        unactivePlayerPanels = new List<GameObject>{playerPanel, archerPanel, knightPanel, spearinPanel, wizardPanel, soldierPanel};
        playerTypes = new List<PlayerType>{ PlayerType.Player, PlayerType.Archer, PlayerType.Knight, PlayerType.Spearin, PlayerType.Wizard, PlayerType.Soldier };
        HideAllPanel();
    }

    private void Start()
    {
        currentIndex = GameDataManager.Instance.SelectedPlayerIndex;
        UpdateButtons(); 

        previousButton.onClick.AddListener(OnPreviousButtonClicked);
        nextButton.onClick.AddListener(OnNextButtonClicked);
    }

    private void ManagePlayerPanels(PlayerType playerType)
    {
        switch (playerType)
        {
            case PlayerType.Player:
                SetActivePlayerPanel(playerPanel);
                break;
            case PlayerType.Archer:
                SetActivePlayerPanel(archerPanel);
                break;
            case PlayerType.Knight:
                SetActivePlayerPanel(knightPanel);
                break;
            case PlayerType.Spearin:
                SetActivePlayerPanel(spearinPanel);
                break;
            case PlayerType.Wizard:
                SetActivePlayerPanel(wizardPanel);
                break;
            case PlayerType.Soldier:
                SetActivePlayerPanel(soldierPanel);
                break;
        }
    }

    private void SetActivePlayerPanel(GameObject panel)
    {
        DisableActivePlayerPanel();
        unactivePlayerPanels.Remove(panel);
        activePlayerPanel = panel;
        panel.SetActive(true);
    }

    private void HideAllPanel()
    {
        DisableActivePlayerPanel();
        foreach (GameObject panel in unactivePlayerPanels)
        {
            panel.SetActive(false);
        }
    }

    private void DisableActivePlayerPanel()
    {
        if(activePlayerPanel == null) return;
        unactivePlayerPanels.Add(activePlayerPanel);
        activePlayerPanel.SetActive(false);
        activePlayerPanel = null;
    }

    private void OnPreviousButtonClicked()
    {
        if (currentIndex > 0)
        {
            currentIndex--;
            ManagePlayerPanels(playerTypes[currentIndex]);
            UpdateButtons();
        }
        SFXManager.Instance.PlaySound(SoundType.Button, transform);
    }

    private void OnNextButtonClicked()
    {
        if (currentIndex < playerTypes.Count - 1)
        {
            currentIndex++;
            ManagePlayerPanels(playerTypes[currentIndex]);
            UpdateButtons();
        }
        SFXManager.Instance.PlaySound(SoundType.Button, transform);
    }

    private void UpdateButtons()
    {
        previousButton.gameObject.SetActive(currentIndex > 0);
        nextButton.gameObject.SetActive(currentIndex < playerTypes.Count - 1);
    }
}
