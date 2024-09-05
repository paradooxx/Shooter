using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GooglePlayLogin : MonoBehaviour
{
    [SerializeField] private TMP_Text googleUserId;
    [SerializeField] private TMP_Text googleUserName;
    [SerializeField] private TMP_Text googleAvatarURL;

    private Action updateUIAction;

    void Start()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                Debug.Log("Login successful!");

                // Retrieve Player Information
                string userId = Social.localUser.id;                   // Google Player ID
                string userName = Social.localUser.userName;           // Player Display Name
                // string playerEmail = ((PlayGamesLocalUser)Social.localUser).Email;  // Player Email
                string userAvatarUrl = Social.localUser.image != null ? Social.localUser.image.ToString() : "No Avatar";  // Avatar URL

                googleUserId.text = userId;
                googleUserName.text = userName;
                googleAvatarURL.text = userAvatarUrl;
            }
            else
            {
                Debug.Log("Login failed.");
            }
        });
    }

    internal void ProcessAuthentication(SignInStatus signInStatus)
    {
        if (signInStatus == SignInStatus.Success)
        {
            Debug.Log("Sign-in successful");
        }
        else
        {
            Debug.LogError("Sign-in failed");
            Invoke("LoadScene", 5f);
        }
    }

    void LoadScene()
    {
        SceneManager.LoadSceneAsync(1); 
    }

    void Update()
    {
        if (updateUIAction != null)
        {
            updateUIAction.Invoke(); 
            updateUIAction = null; 
        }
    }
}
