using System;
using System.Collections;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] private HealthBar healthBar;
    public int maxPlayerHealth;
    HealthSystem healthSystem;

    [SerializeField] private PlayerStats playerStats;
    [SerializeField] private Renderer characterRenderer;
    [SerializeField] private Color hitColor = Color.red;
    [SerializeField] private float blipDuration = 0.1f;
    [SerializeField] private GameObject deathEffect;
    private Color originalColor;

    private void Awake()
    {
        maxPlayerHealth = playerStats.health;
    }

    private void Start()
    {
        healthSystem = new HealthSystem(maxPlayerHealth);

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
        Instantiate(deathEffect, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        Destroy(gameObject);
    }

    //used when player goes through reward gates
    public void ResetHealth(int mhealth)
    {
        healthSystem = new HealthSystem(mhealth);
        healthBar.Setup(healthSystem);
        healthSystem.OnHealthZero += HealthSystem_OnHealthZero;
    }

    public void TakeDamage(int damageAmount)
    {
        healthSystem.Damage(damageAmount);
        StartCoroutine(BlipEffect());
    }

    public int CurrentHealth()
    {
        return healthSystem.GetHealth();
    }

    private IEnumerator BlipEffect()
    {
        characterRenderer.material.color = hitColor;
        yield return new WaitForSeconds(blipDuration);
        characterRenderer.material.color = originalColor;
    }
}
