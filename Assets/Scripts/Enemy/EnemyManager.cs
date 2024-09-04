using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static List<GameObject> enemies = new List<GameObject>();

    public static void RegisterEnemy(GameObject enemy)
    {
        if(!enemies.Contains(enemy))
        {
            enemies.Add(enemy);
        }
    }

    public static void UnregisterEnemy(GameObject enemy)
    {
        if(!enemies.Contains(enemy))
        {
            enemies.Remove(enemy);
        }
    }

    public static GameObject FindNearestEnemy(Vector3 position)
    {
        enemies.RemoveAll(enemy => enemy == null);

        GameObject nearestEnemy = null;
        float shorestDistance = Mathf.Infinity;

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(position, enemy.transform.position);
            if(distance < shorestDistance)
            {
                shorestDistance = distance;
                nearestEnemy = enemy;
            }
        }    
        return nearestEnemy;
    }

    public static int GetEnemyCount()
    {
        return enemies.Count;
    }
}
