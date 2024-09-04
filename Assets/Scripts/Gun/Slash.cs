using UnityEngine;

public class Slash : MonoBehaviour
{
    public int bulletDamage = 0;
    [SerializeField] private float destroyDelay = 3f;

    private void Update()
    {
        Destroy(gameObject, destroyDelay);
    }
}
