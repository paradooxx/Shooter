using System.Collections;
using UnityEngine;

public class GunHandler : MonoBehaviour
{
    private Transform shootPoint;
    private GameObject enemy;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Vector3 bulletDirection = Vector3.forward;

    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private float  nextFireTime = 0f;
    [SerializeField] private float  yOffset = 2f;

    [SerializeField] private float burstDuration = 2f;
    [SerializeField] private float burstInterval = 0.2f;
    [Range(0, 1)][SerializeField] private float volume = 0.1f;

    private float fireRate;

    private void Start()
    {
        shootPoint = GetComponentInParent<GunSelector>().shootPoint;
        fireRate = GetComponentInParent<GunSelector>().playerController.bulletFireRate;
    }

    private void Update()
    {
        if (Time.time > nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + 1 / fireRate;
        }
    }

    private void FireBullet()
    {
        FindNearestEnemy();

        GameObject bullet = Instantiate(bulletPrefab, shootPoint.position, Quaternion.identity);
        Rigidbody rb = bullet.GetComponent<Rigidbody>();

        if (rb != null)
        {
            if (enemy != null)
            {
                //float distanceToEnemy = Vector3.Distance(shootPoint.position, enemy.transform.position);
                Vector3 directionToTarget;
                Vector3 adjustedTargetPos = enemy.transform.position;
                adjustedTargetPos.y += yOffset;
                directionToTarget = (adjustedTargetPos - shootPoint.position).normalized;
                bullet.transform.rotation = Quaternion.LookRotation(directionToTarget);
                rb.velocity = directionToTarget * bulletSpeed;             
            }
            else
            {
                rb.velocity = bulletDirection.normalized * bulletSpeed;
                bullet.transform.rotation = Quaternion.identity;
            }
        }
        SFXManager.Instance.PlaySound(SoundType.Gun, transform, volume);
    }

    private void SpecialAttack()
    {
        StartCoroutine(BurstAttack());
    }

    private IEnumerator BurstAttack()
    {
        float elapsedTime = 0f;
        while (elapsedTime < burstDuration)
        {
            FireBullet();
            elapsedTime += burstInterval;
            yield return new WaitForSeconds(burstInterval);
        }
    }

    public void ChangeFireRate(float speed)
    {
        fireRate = speed;
    }

    private void FindNearestEnemy()
    {
        enemy = EnemyManager.FindNearestEnemy(transform.position);
    }
}
