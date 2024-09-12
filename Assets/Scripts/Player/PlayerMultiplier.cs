using UnityEngine;

public class PlayerMultiplier : MonoBehaviour
{
    [Header("Player Objects")]
    [SerializeField] private GameObject playerGameObject;
    [SerializeField] private GameObject archerGameObject;
    [SerializeField] private GameObject knightGameObject;
    [SerializeField] private GameObject spearinGameObject;
    [SerializeField] private GameObject wizardGameObject;
    [SerializeField] private GameObject soldierGameObject;

    [Header("Others")]
    [SerializeField] private float spawnRadius = 3.0f;
    [SerializeField] private int maxAttempts = 10;
    [SerializeField] private LayerMask collisionMask;
    [SerializeField] private GameObject spawnEffect;
    [SerializeField] private string barrelTag;

    private GameObject playerPlatform;
    private GameObject Player;

    private void Start()
    {
        playerPlatform = GameObject.FindWithTag("PlayerPlatform");
    }

    public void PlayerInstant()
    {
        if (barrelTag == "Player")
        {
            PlayerInstantiate(playerGameObject);
        }
        else if(barrelTag == "Archer")
        {
            PlayerInstantiate(archerGameObject);
        }
        else if(barrelTag == "Knight")
        {
            PlayerInstantiate(knightGameObject);
        }
        else if(barrelTag == "Spearin")
        {
            PlayerInstantiate(spearinGameObject);
        }
        else if(barrelTag == "Wizard")
        {
            PlayerInstantiate(wizardGameObject);
        }
        else if(barrelTag == "Soldier")
        {
            PlayerInstantiate(soldierGameObject);
        }
    }

    private void SpawnEffects(Vector3 colliderTransform)
    {
        GameObject effect = Instantiate(spawnEffect, colliderTransform + new Vector3(0, 0.5f, 0), Quaternion.identity);
        effect.transform.SetParent(null);
    }

    private void PlayerInstantiate(GameObject player)
    {
        FindNearestPlayer();
        Vector3 spawnPosition;

        for (int i = 0; i < maxAttempts; i++)
        {
            spawnPosition = Player.transform.position + (Random.insideUnitSphere * spawnRadius);
            spawnPosition.y = Player.transform.position.y;

            if (!Physics.CheckSphere(spawnPosition, Player.transform.localScale.x / 2, collisionMask))
            {
                Instantiate(player, spawnPosition, Quaternion.Euler(0, 0, 0)).transform.parent = playerPlatform.transform;
                SpawnEffects(spawnPosition);
                SFXManager.Instance.PlaySound(SoundType.Summon, transform);
                return;
            }
        }
    }

    private void FindNearestPlayer()
    {
        Player = PlayerManager.FindNearestPlayer(transform.position);
    }
}
