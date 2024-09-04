using UnityEngine;
using UnityEngine.Animations;

public class ProjectileHandler : MonoBehaviour
{
    [SerializeField] private Transform shootPoint;
    private GameObject enemy;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Vector3 bulletDirection = Vector3.forward;

    [SerializeField] private float bulletSpeed = 50f;
    [SerializeField] private float yOffset = 2f;

    private void FireBall()
    {
        FindNearestEnemy();

        GameObject fireBall = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = fireBall.GetComponent<Rigidbody>();

        if (rb != null)
        {
            if (enemy != null)
            {
                Vector3 adjustedTargetPos = enemy.transform.position;
                adjustedTargetPos.y += yOffset;
                Vector3 directionToTarget = (adjustedTargetPos - shootPoint.position).normalized;
                fireBall.transform.rotation = Quaternion.LookRotation(directionToTarget);
                rb.velocity = directionToTarget * bulletSpeed;
            }
            else
            {
                rb.velocity = bulletDirection.normalized * bulletSpeed;
                fireBall.transform.rotation = Quaternion.identity;
            }
        }
        PlayerBulletSoundEffect();
    }

    private void ThrowSpear()
    {
        FindNearestEnemy();

        GameObject spear = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
        Rigidbody rb = spear.GetComponent<Rigidbody>();

        if (rb != null)
        {
            if (enemy != null)
            {
                Vector3 adjustedTargetPos = enemy.transform.position;
                adjustedTargetPos.y += yOffset;
                Vector3 directionToTarget = (adjustedTargetPos - shootPoint.position).normalized;
                spear.transform.rotation = Quaternion.LookRotation(directionToTarget) * Quaternion.Euler(new Vector3(90, spear.transform.rotation.y, spear.transform.rotation.z));
                rb.velocity = directionToTarget * bulletSpeed;
                PlayerBulletSoundEffect();
            }
            else
            {
                Vector3 shootDirection = shootPoint.forward;
                rb.velocity = shootDirection * bulletSpeed;
                spear.transform.rotation = Quaternion.LookRotation(shootDirection);
                PlayerBulletSoundEffect();
            }
        } 
    }

    private void PlayerBulletSoundEffect()
    {
        if(gameObject.name == "Player") return;
        else if(gameObject.name == "archer(Clone)")
        {
            SFXManager.Instance.PlaySound(SoundType.Arrow, transform, 0.2f);
        }
        else if(gameObject.name == "Knight(Clone)")
        {
            SFXManager.Instance.PlaySound(SoundType.Sword, transform);
        }
        else if(gameObject.name == "Spearin(Clone)")
        {
            SFXManager.Instance.PlaySound(SoundType.Spear, transform, 0.2f);
        }
        else if(gameObject.name == "wizard(Clone)")
        {
            SFXManager.Instance.PlaySound(SoundType.Fire, transform);
        }
    }

    private void FindNearestEnemy()
    {
        enemy = EnemyManager.FindNearestEnemy(transform.position);
    }
}