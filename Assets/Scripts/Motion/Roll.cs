using UnityEngine;

public class Roll : MonoBehaviour
{
    [SerializeField] private float rollSpeed = 100f;
    private Rigidbody rb;
    
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        Roller();
    }

    private void Roller()
    {
        float rollAmount = rollSpeed * Time.deltaTime;
        Vector3 rollRotation = new Vector3(0, rollAmount, 0);
        
        rb.MoveRotation(rb.rotation * Quaternion.Euler(rollRotation));
    }
}
