using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InputFieldTest : MonoBehaviour
{
    public TMP_InputField inputField; 
    public GameObject targetObject; 
    public int minthresholdValue = 10;
    public int maxthresholdValue = 100; 

    void Start()
    {
        // CheckInputValue(inputField.text);
        targetObject.SetActive(false);
        
        inputField.onValueChanged.AddListener(delegate { CheckInputValue(inputField.text); });
        targetObject.GetComponent<Button>().onClick.AddListener(() => {SceneManager.LoadSceneAsync(1);});
    }

    void CheckInputValue(string input)
    {
        int value;
        if (int.TryParse(input, out value))
        {
            targetObject.SetActive(value > minthresholdValue && value < maxthresholdValue);
        }
        else
        {
            
            targetObject.SetActive(false);
        }
    }
}
