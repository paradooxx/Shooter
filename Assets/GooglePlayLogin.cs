using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class GooglePlayLogin : MonoBehaviour
{
    [SerializeField] private TMP_Text googleUserId;
    [SerializeField] private TMP_Text googleUserName;
    [SerializeField] private TMP_Text googleAvatarURL;
    [SerializeField] private TMP_Text successText;
    [SerializeField] private TMP_Text statusText;

    [SerializeField] private GameObject confirmAgePanel;
    [SerializeField] private GameObject standbyPanel;
    [SerializeField] private GameObject retryLoginPanel;

    [SerializeField] private Button retryButton;
    
    [SerializeField] private Image avatarImage;

    private const string firstTimeLoginStatus = "firstTimeLoginStatus";

    private bool firstTimeLogin = false;

    void InitializePlayGamesLogin()
    {
        var config = new PlayGamesClientConfiguration.Builder()
            // Requests an ID token be generated.  
            // This OAuth token can be used to
            // identify the player to other services such as Firebase.
            .RequestIdToken()
            .Build();

        PlayGamesPlatform.InitializeInstance(config);
        PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();
    }

    void LoginGoogle()
    {
        Social.localUser.Authenticate(OnGoogleLogin);
    }

    void OnGoogleLogin(bool success)
    {
        if (success)
        {
            // Call Unity Authentication SDK to sign in or link with Google.
            Debug.Log("Login with Google done. IdToken: " + ((PlayGamesLocalUser)Social.localUser).GetIdToken());
        }
        else
        {
            Debug.Log("Unsuccessful login");
        }
    }

    void LoadScene()
    {
        SceneManager.LoadSceneAsync(1); 
    }
}
