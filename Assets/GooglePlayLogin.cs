using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GooglePlayLogin : MonoBehaviour
{
    [SerializeField] private TMP_Text googleUserId;
    [SerializeField] private TMP_Text googleUserName;
    [SerializeField] private TMP_Text googleAvatarURL;

    [SerializeField] private GameObject confirmAgePanel;

    [SerializeField] private Button retryButton;

    private const string firstTimeLoginStatus = "firstTimeLoginStatus";

    private bool firstTimeLogin = true;

    private void Awake()
    {
        firstTimeLogin = PlayerPrefs.GetInt(firstTimeLoginStatus, 1) == 1;
    }

    private void Start()
    {
        retryButton.onClick.AddListener(() => SignIn());
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus signInStatus)
    {
        if (signInStatus == SignInStatus.Success)
        {
            if(firstTimeLogin)
            {
                confirmAgePanel.SetActive(true);
                PlayerPrefs.SetInt(firstTimeLoginStatus, firstTimeLogin ? 1 : 0);
            }
            else
            {
                confirmAgePanel.SetActive(false);
                Invoke("LoadScene", 5f);
            }
        }
        else
        {
            Debug.LogError("Sign-in failed");
        }
    }

    void LoadScene()
    {
        SceneManager.LoadSceneAsync(1); 
    }
}
