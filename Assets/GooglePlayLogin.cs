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

    private void Awake()
    {
        // firstTimeLogin = PlayerPrefs.GetInt(firstTimeLoginStatus, 1) == 1;
    }

    private void Start()
    {
        PlayGamesPlatform.Activate();
        retryButton.onClick.AddListener(() => SignIn());
        ShowStandbyScreen("Authenticating...");
        // Social.localUser.Authenticate (ProcessAuthentication);
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
                HideStandbyScreen();
                confirmAgePanel.SetActive(true);
                // PlayerPrefs.SetInt(firstTimeLoginStatus, firstTimeLogin ? 1 : 0);
            }
            else
            {
                confirmAgePanel.SetActive(false);
                Invoke("LoadScene", 5f);
            }
            googleUserName.gameObject.SetActive(true);
            googleUserId.gameObject.SetActive(true);
            googleAvatarURL.gameObject.SetActive(true);
            googleUserName.text = Social.localUser.userName;
            googleUserId.text = Social.localUser.id;
            Texture2D texture2D = Social.localUser.image;
            // avatarImage.sprite = Sprite.Create(texture2D, new Rect (0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0f, 0f));
            successText.text = "Succesful";
        }
        else
        {
            Debug.LogError("Sign-in failed");
            ShowStandbyScreen("Authentication Failed. Please restart the app.");
            retryLoginPanel.gameObject.SetActive(true);
            successText.text = "Failed!";
        }
    }

    private void ShowStandbyScreen(string message)
    {
        standbyPanel.SetActive(true);
        if (statusText != null)
        {
            statusText.text = message;
        }
    }

    private void HideStandbyScreen()
    {
        standbyPanel.SetActive(false);
    }

    //use if user avatar is required
    private IEnumerator DownloadImage(string url)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(url);
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Texture2D texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
            if (texture != null)
            {
                Sprite avatarSprite = Sprite.Create(texture, new Rect(0, 0, texture.width, texture.height), new Vector2(0.5f, 0.5f));
                avatarImage.sprite = avatarSprite; // Set the downloaded avatar in the UI Image
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadSceneAsync(1); 
    }
}
