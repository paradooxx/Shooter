using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private GameObject player;         
    [SerializeField] private float rotationSpeed = 2f;
    [SerializeField] private int enemyDamage = 10;
    [SerializeField] private bool isBoss;
    public float defaultMoveSpeed;
    private bool attackPlayer = false;

    private float moveSpeed;

    private Animator animator; 

    private void OnEnable()
    {
        EnemyManager.RegisterEnemy(gameObject);
        //EnemySpawner.totalEnemies++;
    }

    private void OnDisable()
    {
        EnemyManager.UnregisterEnemy(gameObject);
        EnemySpawner.totalEnemies--;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        FindNearestPlayer();
        
        //SFXManager.Instance.FootStepSound();
    }

    private void Update()
    {
        FindNearestPlayer();
        if(player == null)
        {
            moveSpeed = 0;
            animator.SetBool("attack", false);
            animator.speed = 1f;
        }
        else if (player != null)
        {
            MoveTowardsPlayer();
        }
    }

    private void MoveTowardsPlayer()
    {
        moveSpeed = defaultMoveSpeed;
        ChasePlayer();
    }

    private void ChasePlayer()
    {
        Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;

        Quaternion targetRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        transform.position += directionToPlayer * moveSpeed * Time.deltaTime;

    }

    private void AttackPlayer()
    {
        if(isBoss)
        {
            animator.SetBool("attack", true);
            animator.speed = 2f;
        }
        else
        {
            animator.SetBool("attack", true);
            animator.speed = 1f;
        }
        moveSpeed = 0f;
    }  

    private void ReduceHealth()
    {
        if(attackPlayer)
        {
            var playerDamage = player.GetComponent<PlayerHealth>();
            playerDamage.TakeDamage(enemyDamage);
            //add effects here later on
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            attackPlayer = true;
            AttackPlayer();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            attackPlayer = false;
        }
    }

    private void FindNearestPlayer()
    {
        if(GameStateManager.CurrentGameState != GameState.Game)
        {
            return;
        }
        
        player = PlayerManager.FindNearestPlayer(transform.position);

        if(player == null)
        {
            GameStatus.Instance.onGameLoose?.Invoke();
        }
    }
}
