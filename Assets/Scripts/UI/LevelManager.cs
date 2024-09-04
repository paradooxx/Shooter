using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void StartLevel()
    {
        SceneManager.LoadSceneAsync(1);
    }

    public void LoadLevel(int index)
    {
        SceneManager.LoadSceneAsync(index);
    }
}
