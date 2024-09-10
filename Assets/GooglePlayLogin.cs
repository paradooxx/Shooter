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
        PlayGamesPlatform.Instance.Authenticate((success) =>{
        {
            if (success == SignInStatus.Success)
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
                    // Invoke("LoadScene", 5f);
                }
                // avatarImage.sprite = Sprite.Create(texture2D, new Rect (0.0f, 0.0f, texture2D.width, texture2D.height), new Vector2(0f, 0f));
                successText.text = "Succesful";
                PlayGamesPlatform.Instance.RequestServerSideAccess(true, code =>
                    {
                        Debug.Log("Authorization code: " + code);
                        googleUserId.text = code;
    // This token serves as an example to be used for SignInWithGooglePlayGames
                    });
            }
            else
            {
                Debug.Log("Sign-in failed");
                ShowStandbyScreen("Authentication Failed. Please restart the app.");
                retryLoginPanel.gameObject.SetActive(true);
                successText.text = "Failed!";
            }
        }
    });
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

    private IEnumerator SendCodeToServer(string code)
    {
        string serverUrl = "https://dbf8-2400-1a00-b1e0-c698-8d75-6348-da12-179b.ngrok-free.app/api/games/nightfall/users/add";

        // Create the JSON payload
        string jsonData = JsonUtility.ToJson(new { authorization_code = code });

        // Create a new UnityWebRequest with a POST method and set the request headers for JSON
        using (UnityWebRequest www = new UnityWebRequest(serverUrl, "POST"))
        {
            // Set the request body
            byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);
            www.uploadHandler = new UploadHandlerRaw(bodyRaw);
            www.downloadHandler = new DownloadHandlerBuffer();

            // Set the request headers for JSON
            www.SetRequestHeader("Content-Type", "application/json");

            // Send the request and wait for a response
            yield return www.SendWebRequest();

            if (www.result == UnityWebRequest.Result.ConnectionError || www.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError("Error sending code to server: " + www.error);
                statusText.text = "Error sending code to server.";
            }
            else
            {
                Debug.Log("Code sent to server successfully. Response: " + www.downloadHandler.text);
                statusText.text = "Code sent to server successfully.";
            }
        }
    }

    void LoadScene()
    {
        SceneManager.LoadSceneAsync(1); 
    }
}