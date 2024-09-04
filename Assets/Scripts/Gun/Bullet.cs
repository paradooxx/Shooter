using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int bulletDamage = 0;
    [SerializeField] private float destroyDelay;
    public bool isSlash;
    public static int bulletDamageModifier = 1;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Barrel"))
        {
            Destroy(gameObject);
        }
    }

    private void Update()
    {
        Destroy(gameObject, destroyDelay);
    }


}
