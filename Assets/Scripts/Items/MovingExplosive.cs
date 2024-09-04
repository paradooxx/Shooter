using UnityEngine;

public class MovingExplosive : MonoBehaviour
{
    [SerializeField] private float explosionRadius = 2f;
    [SerializeField] private float  explosionDamage = 60f;
    [SerializeField] private LayerMask explosionLayerMask;
    [SerializeField] private GameObject explosionParticles;
    
    private void Explode()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius, explosionLayerMask);

        foreach (Collider hit in colliders)
        {
            PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
            if (playerHealth != null)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                float damageFactor = 1 - (distance / explosionRadius);
                int damage = Mathf.RoundToInt(damageFactor * explosionDamage);
                Instantiate(explosionParticles, transform.position, Quaternion.identity);
                playerHealth.TakeDamage(damage);
            }
        }
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            Explode();
            SFXManager.Instance.PlaySound(SoundType.Explosion, transform);
        }
    }
}
