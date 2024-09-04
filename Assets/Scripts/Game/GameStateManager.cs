using UnityEngine;
using UnityEngine.Events;

public class GameStateManager : MonoBehaviour
{
    [SerializeField] private GameState defaultState;

    public static GameStateManager Instance { get; private set; }
    public static GameState CurrentGameState { get; private set; }

    public static event UnityAction<GameState> OnStateChange;

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    private void Start()
    {
        SetState(defaultState);
    }

    public void SetState(GameState state)
    {
        CurrentGameState = state;
        OnStateChange?.Invoke(state);
    }
}

public enum GameState
{
    Win,
    Loose,
    SelectPlayer,
    Game,
    MainMenu,
    Settings,
    Shop,
    NoState,
    Perks,
    Wheel,
    Achievement,
    NoAds,
    SpecialOffer
}
