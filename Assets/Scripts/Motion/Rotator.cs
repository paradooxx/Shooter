using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private RotationAxis rotationAxis;

    private Vector3 rotationVector;

    private void Start()
    {
        switch(rotationAxis)
        {
            case RotationAxis.Up:
                rotationVector = Vector3.up;
                break;
            case RotationAxis.Right:
                rotationVector = Vector3.right;
                break;
            case RotationAxis.Froward:
                rotationVector = Vector3.forward;
                break;
        }
    }

    void Update()
    {
        transform.Rotate(speed * Time.deltaTime * rotationVector);
    }
}

public enum RotationAxis
{
    Up = 0,
    Right = 1,
    Froward = 2
}
