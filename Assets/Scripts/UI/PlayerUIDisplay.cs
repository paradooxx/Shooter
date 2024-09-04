using UnityEngine;

public class PlayerUIDisplay : MonoBehaviour
{
    [SerializeField] private GameObject[] playerGameObjects;

    private void Start()
    {
        ChangePlayerUIDisplay();
    }

    public void ChangePlayerUIDisplay()
    {
        int selectedIndex = GameDataManager.Instance.SelectedPlayerIndex;
        for (int i = 0; i < playerGameObjects.Length; i++)
        {
            playerGameObjects[i].SetActive(i == selectedIndex);
        }
    }
}
