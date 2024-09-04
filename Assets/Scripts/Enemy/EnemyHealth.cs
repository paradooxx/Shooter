using System;
using System.Collections;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    [SerializeField] private int maxEnemyHealth = 100;
    [SerializeField] private GameObject healthBarObject;
    [SerializeField] private GameObject bloodParticles;

    [SerializeField] private Renderer characterRenderer;
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float blipDuration = 0.1f;
    private Color originalColor;
    private HealthSystem healthSystem;

    void Start()
    {
        healthSystem = new HealthSystem(maxEnemyHealth);

        healthBar.Setup(healthSystem);
        healthSystem.OnHealthZero += HealthSystem_OnHealthZero;

        if (characterRenderer != null)
        {
            originalColor = characterRenderer.material.color;
        }
        else
        {
            return;
        }
    }

    private void HealthSystem_OnHealthZero(object sender, EventArgs e)
    {
        Instantiate(bloodParticles, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        GameDataManager.Instance.EnemyKilledCount ++;
        Destroy(gameObject);
    }

    public void TakeDamage(int damageAmount)
    {
        healthBarObject.SetActive(true);
        healthSystem.Damage(damageAmount);
        StartCoroutine(BlipEffect());
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.CompareTag("Bullet"))
        {
            var bullet = collision.gameObject.GetComponent<Bullet>();
            TakeDamage(bullet.bulletDamage * Bullet.bulletDamageModifier);
            if(!bullet.isSlash)
            {
                Destroy(collision.gameObject);
            }
        }
    }

    private IEnumerator BlipEffect()
    {
        characterRenderer.material.color = hitColor;
        yield return new WaitForSeconds(blipDuration);
        characterRenderer.material.color = originalColor;
    }
}
