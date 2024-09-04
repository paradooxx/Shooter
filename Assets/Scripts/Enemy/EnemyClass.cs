using UnityEngine;

[System.Serializable]
public class EnemyPrefab
{
    public GameObject prefab;
    public int count;

    public void SetCount(int newCount)
    {
        count = newCount;
    }

    public int GetCount()
    {
        return count;
    }

    public void DecreaseCount()
    {
        if (count > 0)
        {
            count--;
        }
    }
}

[System.Serializable]
public class BossPrefab
{
    public GameObject prefab;
    public int count;

    public void SetCount(int newCount)
    {
        count = newCount;
    }

    public int GetCount()
    {
        return count;
    }
}