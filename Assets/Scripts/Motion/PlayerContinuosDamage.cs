using System.Collections;
using UnityEngine;

public class PlayerContinuosDamage : MonoBehaviour
{
    [SerializeField] private float damageInterval;
    [SerializeField] private int damage;

    [Tooltip("If this is electric obstacle, enable it")]
    [SerializeField] private bool isElectric;
    private bool isDamaging;
    
    private void Start()
    {
        if(!isElectric) return;
        else SFXManager.Instance.PlaySound(SoundType.Electric, transform);
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            StartCoroutine(ApplyDamage(collider.gameObject));
        }
    }

    private IEnumerator ApplyDamage(GameObject player)
    {
        isDamaging = true;
        PlayerHealth playerHealth = player.GetComponent<PlayerHealth>();
        while (isDamaging)
        {
            playerHealth.TakeDamage(damage);
            yield return new WaitForSeconds(damageInterval);
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.gameObject.CompareTag("Player"))
        {
            isDamaging = false;
        }
    }
}
