using UnityEngine;

public class MoveP2P : MonoBehaviour
{
    [SerializeField] private Transform pointA;
    [SerializeField] private Transform pointB;
                                    
    [SerializeField] private float speed = 1.0f;

    private void Update()
    {
        MoveBetweenPoints();
    }

    private void MoveBetweenPoints()
    {
        float time = Mathf.PingPong(Time.time * speed, 1.0f);
        transform.position = Vector3.Lerp(pointA.position, pointB.position, time);
    }
}
