using TMPro;
using UnityEngine;

public class InputFieldTest : MonoBehaviour
{
    public TMP_InputField inputField; 
    public GameObject targetObject; 
    public int thresholdValue = 10; 

    void Start()
    {
        CheckInputValue(inputField.text);
        
        inputField.onValueChanged.AddListener(delegate { CheckInputValue(inputField.text); });
    }

    void CheckInputValue(string input)
    {
        int value;
        if (int.TryParse(input, out value))
        {
            targetObject.SetActive(value > thresholdValue);
        }
        else
        {
            
            targetObject.SetActive(false);
        }
    }
}
