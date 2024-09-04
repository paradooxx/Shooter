using TMPro;
using UnityEngine;

public class Rewarder : MonoBehaviour
{
    [SerializeField] private TMP_Text expressionText;
    [SerializeField] private TMP_Text rewardText;
    
    public SpriteRenderer spriteRenderer { get; private set; }

    public string Expression { get; private set; }
    public string RewardType { get; private set; }
    public float Number { get; private set; }

    public void Initialize(string rewardType, string expression, float number)
    {
        RewardType = rewardType;
        Expression = expression;
        Number = number;
        rewardText.text = RewardType.ToString();
        expressionText.text = Expression + Number.ToString();
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    public float Calculate(int number)
    {
        switch (Expression)
        {
            case "x":
                return number * Number;
            case "÷":
                return number / Number;
            default:
                break;
        }
        return number;
    }
}
