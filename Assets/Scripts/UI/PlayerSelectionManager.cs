using UnityEngine;
using UnityEngine.Events;

public class PlayerSelectionManager : MonoBehaviour
{
    public static PlayerSelectionManager Instance { get; private set;}
    public static PlayerType CurrentPlayerType { get; private set; }

    public static event UnityAction<PlayerType> OnPlayerChanged;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        
        SetState(GameDataManager.Instance.SelectedPlayerIndex);
        // SetState(GameDataManager.Instance.SelectedPlayerIndex);
    }

    public void SetState(int playerTypeIndex)
    {
        PlayerType playerType = (PlayerType)playerTypeIndex;
        CurrentPlayerType = playerType;
        OnPlayerChanged?.Invoke(playerType);
    }
}

public enum PlayerType
{
    Player,
    Archer,
    Knight,
    Spearin,
    Wizard,
    Soldier
}
