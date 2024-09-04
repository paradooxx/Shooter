using UnityEngine;

[System.Serializable]
public class SVector
{
    public float x;
    public float y;

    public SVector(float x, float y)
    {
        this.x = x;
        this.y = y;
    }

    public SVector(Vector2 vector)
    {
        this.x = vector.x;
        this.y = vector.y;
    }

    public Vector2 ToVector2()
    {
        return new Vector2(x, y);
    }
}