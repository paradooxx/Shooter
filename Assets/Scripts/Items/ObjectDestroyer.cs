using UnityEngine;

public class ObjectDestroyer : MonoBehaviour
{
     private void OnTriggerEnter(Collider collision)
    {
        if (collision.transform.parent != null)
        {
            Destroy(collision.transform.parent.gameObject);
        }
        else
        {
            Destroy(collision.gameObject);
        }
    }
}
