using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private bool isMovingRight;

    public float bulletFireRate;

    private Animator animator;
    private GameObject enemy;

    [SerializeField] private bool isRotationNeeded;
    [SerializeField] private PlayerStats playerStats;

    private void OnEnable()
    {
        PlayerManager.RegisterPlayer(gameObject);
    }

    private void OnDisable()
    {
        PlayerManager.UnregisterPlayer(gameObject);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        bulletFireRate = playerStats.fireRate;
    }

    private void Start()
    {
        animator.speed = bulletFireRate;
    }

    private void Update()
    {
        MovePlayer();
        RotateTowardsNearestEnemy();
        if(GameStateManager.CurrentGameState == GameState.Win)
        {
            bulletFireRate = 0f;
        }
    }

    private void MovePlayer()
    {
        if(!PlayerPlatfromController.isDragging)
        {
            animator.Play("PlayerIdle");
        }
        else if(PlayerPlatfromController.isDragging)
        {
            PlayerWalkAnimation();
        }
    }

    private void PlayerWalkAnimation()
    {
        if (isMovingRight)
        {
            animator.Play("RightWalk", 1);
        }
        else if (!isMovingRight)
        {
            animator.Play("LeftWalk", 1);
        }
    }
    
    private void RotateTowardsNearestEnemy()
    {
        FindNearestEnemy();

        if (enemy != null)
        {
            Vector3 direction = (enemy.transform.position - transform.position).normalized;
            direction.y = 0; 
            Quaternion lookRotation = Quaternion.LookRotation(direction);

            Vector3 adjustedEulerAngles = lookRotation.eulerAngles;
            if(isRotationNeeded)
            {
                adjustedEulerAngles.y += 66f;
            }
            lookRotation = Quaternion.Euler(adjustedEulerAngles);

            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
        }
    }

    public void ChangeAnimationSpeed(float speed)
    {
        animator.speed = speed;
    }

    private void FindNearestEnemy()
    {
        if(GameStateManager.CurrentGameState != GameState.Game)
        {
            return;
        }
        
        enemy = EnemyManager.FindNearestEnemy(transform.position);

        GameStatus.Instance.onGameWin?.Invoke();
    }

    private void TriggerGameWin()
    {
        Invoke("TriggerGameWin", 2f);
    }
}
