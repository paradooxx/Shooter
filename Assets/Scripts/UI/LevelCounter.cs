using UnityEngine;
using TMPro;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private TMP_Text text;
    [SerializeField] private int additionalNumber;
    [SerializeField] private string prefix;

    void Start()
    {
        int tempIndex = LevelDataContainer.Instance.index + 1;
        text.text = prefix + LevelDataContainer.Instance.index;
    }
}
