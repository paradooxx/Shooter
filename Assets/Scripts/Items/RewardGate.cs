using UnityEngine;

public class RewardGate : MonoBehaviour
{
    [SerializeField] private Rewarder rightGate;
    [SerializeField] private Rewarder leftGate;
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private float alpha;

    private float[] randomNumber = {1.6f, 1.7f, 1.8f, 1.9f, 2f};
    private string[] randomExpression = {"x", "÷", "x", "÷", "x", "÷"},
                    rewardTypes = {"Health", "FireRate", "EnemySpeed", "Health", "FireRate", "EnemySpeed", "Health", "FireRate", "EnemySpeed"};

    private string rewardTypeA, rewardTypeB;

    void Awake()
    {
        rewardTypeA = rewardTypes[Random.Range(0, 8)];
        rewardTypeB = rewardTypes[Random.Range(0, 8)];
    }

    private void OnEnable()
    {
        rightGate.Initialize(rewardTypeA, RandomExpression(), RandomNumber());
        leftGate.Initialize(rewardTypeB, RandomExpression(), RandomNumber());
        if(rightGate.Expression == "x" && leftGate.Expression == "x")
        {
            rightGate.Initialize(rewardTypeA, "÷", RandomNumber());
            SetAlpha(leftGate.spriteRenderer, alpha, 0, 1);
            SetAlpha(rightGate.spriteRenderer, alpha, 1, 0);
        }   
        else if(rightGate.Expression == "÷" && leftGate.Expression == "÷")
        {
            rightGate.Initialize(rewardTypeB, "x", RandomNumber());
            SetAlpha(leftGate.spriteRenderer, alpha, 1, 0);
            SetAlpha(rightGate.spriteRenderer, alpha, 0, 1);
        }
        else if (rightGate.Expression == "x" && leftGate.Expression == "÷")
        {
            SetAlpha(leftGate.spriteRenderer, alpha, 1, 0);
            SetAlpha(rightGate.spriteRenderer, alpha, 0, 1);
        }
        else if(rightGate.Expression == "÷" && leftGate.Expression == "x")
        {
            SetAlpha(leftGate.spriteRenderer, alpha, 0, 1);
            SetAlpha(rightGate.spriteRenderer, alpha, 1, 0);
        }
    }

    private string RandomExpression()
    {
        int randomNum = Random.Range(0, 5);
        return randomExpression[randomNum];
    }

    private float RandomNumber()
    {
        int randomNum = Random.Range(0, 4);
        return randomNumber[randomNum];
    }

    public void SetAlpha(SpriteRenderer spriteRenderer, float alpha, float red, float green)
    {
        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer is null.");
            return;
        }

        alpha = Mathf.Clamp01(alpha);
        red = Mathf.Clamp01(red);
        green = Mathf.Clamp01(green);

        Color currentColor = spriteRenderer.color;
        Color newColor = new Color(red, green, currentColor.b, alpha);
        spriteRenderer.color = newColor;
    }


    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collider.GetComponent<PlayerHealth>();

            Vector3 playerPosition = playerHealth.transform.position;
            float distanceToLeftGate = Vector3.Distance(leftGate.transform.position, playerPosition);
            float distanceToRightGate = Vector3.Distance(rightGate.transform.position, playerPosition);

            bool isLeftGateCloser = distanceToLeftGate < distanceToRightGate;
            Rewarder selectedGate = isLeftGateCloser ? leftGate : rightGate;
            string rewardType = isLeftGateCloser ? rewardTypeB : rewardTypeA;

            ApplyReward(selectedGate, rewardType);
        }
    }

    private void ApplyReward(Rewarder gate, string rewardType)
    {
        if (rewardType == "Health")
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
                playerHealth.ResetHealth((int)gate.Calculate(playerHealth.CurrentHealth()));
            }
            
        }
        else if (rewardType == "FireRate")
        {
            GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
            foreach (GameObject player in players)
            {
                PlayerController playerController = player.GetComponent<PlayerController>();
                GunHandler playerGunHandler = player.GetComponentInChildren<GunHandler>();
                float speed = gate.Calculate((int)playerController.bulletFireRate);
                if(playerGunHandler != null)
                {
                    playerGunHandler.ChangeFireRate(speed);
                }
                else
                {
                    playerController.ChangeAnimationSpeed(speed);
                }
            }
            
        }
        else if (rewardType == "EnemySpeed")
        {
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            foreach (GameObject enemy in enemies)
            {
                EnemyController enemyMotion = enemy.GetComponent<EnemyController>();
                enemyMotion.defaultMoveSpeed = gate.Calculate((int)enemyMotion.defaultMoveSpeed);
            }
        }
        /* else if (rewardType == "Damage")
        {
            GameObject[] bullets = GameObject.FindGameObjectsWithTag("Bullet");
        } */

        boxCollider.enabled = false;
        gate.gameObject.SetActive(false);
    }
}
