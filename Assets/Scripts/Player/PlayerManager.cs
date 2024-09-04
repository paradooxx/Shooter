using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public static List<GameObject> players = new List<GameObject>();

    public static void RegisterPlayer(GameObject player)
    {
        if(!players.Contains(player))
        {
            players.Add(player);
        }
    }

    public static void UnregisterPlayer(GameObject player)
    {
        if(!players.Contains(player))
        {
            players.Remove(player);
        }
    }

    public static GameObject FindNearestPlayer(Vector3 position)
    {
        players.RemoveAll(player => player == null);

        GameObject nearestPlayer = null;
        float shortestDistance = Mathf.Infinity;

        foreach(GameObject player in players)
        {
            float distance = Vector3.Distance(position, player.transform.position);
            if(distance < shortestDistance)
            {
                shortestDistance = distance;
                nearestPlayer = player; 
            }
        }
        return nearestPlayer;
    }

    public static int GetPlayerCount()
    {
        return players.Count;
    }
}
