using UnityEngine;

public class PlayerDamageZone : MonoBehaviour
{
    [SerializeField] private int damage = 40;

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.CompareTag("Player"))
        {
            PlayerHealth playerHealth = collision.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(damage);
            //Debug.Log("DamageVibration");
        }
    }
}
