using UnityEngine;

public class PlayerPanelDisplay : MonoBehaviour
{
    public static PlayerPanelDisplay Instance { get; private set;}

    private void Awake()
    {
        if (Instance != null && Instance != this)
            Destroy(this);
        else
            Instance = this;
    }

    [SerializeField] private GameObject[] playerGameObjects;

    public void ChangePlayerPanelDisplay(int currentIndex)
    {
        for (int i = 0; i < playerGameObjects.Length; i++)
        {
            playerGameObjects[i].SetActive(i == currentIndex);
        }
    }
}
