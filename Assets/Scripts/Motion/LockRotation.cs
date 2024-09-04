using UnityEngine;

public class LockRotation : MonoBehaviour
{
    private Quaternion fixedLocalRotation;

    private void Awake()
    {
        // Store the initial local rotation
        fixedLocalRotation = transform.localRotation;
    }

    private void LateUpdate()
    {
        // Maintain the initial local rotation
        transform.localRotation = fixedLocalRotation;
    }
}
