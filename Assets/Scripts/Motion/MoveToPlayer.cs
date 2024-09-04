using System;
using UnityEngine;

public class MoveToPlayers : MonoBehaviour
{
    [SerializeField] private string targetTag = "Player"; 
    [SerializeField] private float moveSpeed = 2f;

    private void Update()
    {
        if (gameObject.transform.parent == null)
        {
            MoveTowardsPlayers();
        }
    }

    private void MoveTowardsPlayers()
    {
        GameObject[] players = GameObject.FindGameObjectsWithTag(targetTag);
        if (players.Length == 0)
        {
            return;
        }

        Vector3 centerPoint = CalculateCenterPoint(players);
        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, centerPoint, step);
    }

    private Vector3 CalculateCenterPoint(GameObject[] players)
    {
        Vector3 centerPoint = Vector3.zero;
        foreach (GameObject player in players)
        {
            centerPoint += player.transform.position;
        }
        centerPoint /= players.Length;
        return centerPoint;
    }
}
