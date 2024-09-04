using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonHandler : MonoBehaviour
{
    private GameStateManager gameStateManager;
    private PlayerChoice playerChoice;

    private void Start()
    {
        gameStateManager = GameStateManager.Instance;
        playerChoice = PlayerChoice.Instance;   
    }

    public void SelectPlayerMenu()
    {
        gameStateManager.SetState(GameState.SelectPlayer);
        ButtonSound();
    }

    public void StartGame()
    {
        playerChoice.StartGame();
        gameStateManager.SetState(GameState.Game);
        ButtonSound();
    }

    public void MainMenu()
    {
        gameStateManager.SetState(GameState.MainMenu);
        ButtonSound();
    }

    public void Settings()
    {
        gameStateManager.SetState(GameState.Settings);
        ButtonSound();
    }

    public void PerkPanel()
    {
        gameStateManager.SetState(GameState.Perks);
        ButtonSound();
    }

    public void ShopPanel()
    {
        gameStateManager.SetState(GameState.Shop);
        ButtonSound();
    }

    public void WheelPanel()
    {
        gameStateManager.SetState(GameState.Wheel);
        ButtonSound();
    }

    public void AchievementPanel()
    {
        gameStateManager.SetState(GameState.Achievement);
        ButtonSound();
    }

    public void NoAdsPanel()
    {
        gameStateManager.SetState(GameState.NoAds);
        ButtonSound();
    }

    public void SpecialOfferPanel()
    {
        gameStateManager.SetState(GameState.SpecialOffer);
        ButtonSound();
    }

    public void Restart([SerializeField] GameObject obj)
    {
        int randomNum = Random.Range(0, 5);
        // int randomNum = 4;
        if(randomNum == 4)
        {
            obj.SetActive(true);
        }
        else
        {
            SceneManager.LoadSceneAsync(0);
            AdManager.Instance.ShowInterstitialAd();
        }
        ButtonSound();  
    }

    public void Restart()
    {
        SceneManager.LoadSceneAsync(0);
        AdManager.Instance.ShowInterstitialAd();
        ButtonSound();
    }

    private void ButtonSound()
    {
        SFXManager.Instance.PlaySound(SoundType.Button, transform);
    }

    public void Quit()
    {
        #if UNITY_EDITOR
                EditorApplication.isPlaying = false;
        #else
                Application.Quit();
        #endif
    }
}
