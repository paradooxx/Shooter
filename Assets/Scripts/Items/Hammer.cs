using UnityEngine;

public class DeadlyHammer : MonoBehaviour
{
    [SerializeField] private float blastRadius;
    [SerializeField] private float blastForce;
    [SerializeField] private Transform blastPoint;
    [SerializeField] private GameObject particles;
    [SerializeField] private AudioSource audioSource;

    /* private void PlaySound()
    {
        audioSource.Play();
    } */

    private void Punch()
    {
        Collider[] nearbyColliders = Physics.OverlapSphere(blastPoint.position, blastRadius);
        foreach (Collider nearbyCollider in nearbyColliders)
        {
            if (nearbyCollider.TryGetComponent<Rigidbody>(out Rigidbody rigidbody))
                rigidbody.AddExplosionForce(blastForce, blastPoint.position, blastRadius);
        }
        Instantiate(particles, blastPoint.position, Quaternion.identity);
        SFXManager.Instance.PlaySound(SoundType.Hammer, transform);
    }
}
