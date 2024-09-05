using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using UnityEngine.SceneManagement;

public class GooglePlayLogin : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        SignIn();
    }

    public void SignIn()
    {
        PlayGamesPlatform.Instance.Authenticate(ProcessAuthentication);
    }

    internal void ProcessAuthentication(SignInStatus signInStatus)
    {
        if(signInStatus == SignInStatus.Success)
        {
            Application.Quit();
        }
        else
        {
            SceneManager.LoadSceneAsync(1);
        }
    }
}
